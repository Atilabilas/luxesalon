"""Crawl local server HTML and save evidence. Uses Python's standard library."""
import json
import sys
import time
import xml.etree.ElementTree as ET
from collections import deque
from html.parser import HTMLParser
from pathlib import Path
from urllib.parse import urljoin, urlsplit
from urllib.robotparser import RobotFileParser
from urllib.request import urlopen, build_opener, HTTPRedirectHandler
from urllib.error import HTTPError
from types import SimpleNamespace

class AuditHTML(HTMLParser):
    def __init__(self, text):
        super().__init__(convert_charrefs=True)
        self.headings, self.images, self.links, self.json_ld = [], [], [], []
        self.metas, self.canonical, self.loader = {}, "", False
        self.in_h1, self.in_json = False, False
        self.feed(text)
    def handle_starttag(self, tag, attrs):
        attrs = dict(attrs)
        if tag == "h1":
            self.headings.append("")
            self.in_h1 = True
        if tag == "meta":
            self.metas[attrs.get("name", "").lower()] = attrs.get("content", "")
        if tag == "link" and attrs.get("rel") == "canonical":
            self.canonical = attrs.get("href", "")
        if tag == "a" and "href" in attrs:
            self.links.append(attrs["href"])
        if tag == "img":
            self.images.append(attrs)
        if attrs.get("id") == "loading-overlay":
            self.loader = True
        if tag == "script" and attrs.get("type") == "application/ld+json":
            self.in_json = True
            self.json_ld.append("")
    def handle_endtag(self, tag):
        if tag == "h1":
            self.in_h1 = False
        if tag == "script":
            self.in_json = False
    def handle_data(self, data):
        if self.in_h1:
            self.headings[-1] += data
        if self.in_json:
            self.json_ld[-1] += data

class NoRedirect(HTTPRedirectHandler):
    def redirect_request(self, req, fp, code, msg, headers, newurl):
        return None

def fetch(url, timeout=20, allow_redirects=True):
    try:
        response = (urlopen(url, timeout=timeout) if allow_redirects else build_opener(NoRedirect).open(url, timeout=timeout))
    except HTTPError as error:
        response = error
    with response:
        body = response.read()
        return SimpleNamespace(status=response.code, headers=response.headers, content=body, text=body.decode("utf-8", errors="replace"))

base = sys.argv[1] if len(sys.argv) > 1 else "http://localhost:5100"
errors, pages, graph, checked_assets = [], [], {}, set()
descriptions = {}
sitemap = fetch(base + "/sitemap.xml")
assert sitemap.status == 200, f"sitemap HTTP {sitemap.status}"
urls = [el.text for el in ET.fromstring(sitemap.content).findall("{*}url/{*}loc")]
origin = urlsplit(urls[0]).netloc
paths = {urlsplit(url).path or "/" for url in urls}
robots = RobotFileParser()
robots_response = fetch(base + "/robots.txt")
assert robots_response.status == 200
robots.parse(robots_response.text.splitlines())

for url in urls:
    path = urlsplit(url).path or "/"
    started = time.perf_counter()
    response = fetch(base + path, allow_redirects=False)
    elapsed_ms = round((time.perf_counter() - started) * 1000)
    if response.status != 200:
        errors.append(f"{path}: HTTP {response.status}")
        continue
    page = AuditHTML(response.text)
    if len(page.headings) != 1:
        errors.append(f"{path}: {len(page.headings)} H1")
    description = page.metas.get("description", "").strip()
    if not description:
        errors.append(f"{path}: missing meta description")
    elif description in descriptions:
        errors.append(f"{path}: duplicate meta description with {descriptions[description]}")
    else:
        descriptions[description] = path
    if page.canonical != url:
        errors.append(f"{path}: incorrect canonical")
    if "noindex" in page.metas.get("robots", ""):
        errors.append(f"{path}: noindex")
    if "noindex" in response.headers.get("X-Robots-Tag", "").lower():
        errors.append(f"{path}: X-Robots-Tag noindex")
    if not robots.can_fetch("Googlebot", url):
        errors.append(f"{path}: Googlebot blocked")
    if page.loader:
        errors.append(f"{path}: artificial loading screen")
    for script in page.json_ld:
        try:
            json.loads(script)
        except json.JSONDecodeError:
            errors.append(f"{path}: invalid JSON-LD")
    for img in page.images:
        src = img.get("src", "")
        if "alt" not in img:
            errors.append(f"{path}: image without alt: {src}")
        if not img.get("width") or not img.get("height"):
            errors.append(f"{path}: image without dimensions: {src}")
        if src.startswith("/") and src not in checked_assets:
            checked_assets.add(src)
            asset = fetch(base + src)
            if asset.status != 200:
                errors.append(f"{path}: broken image {src}")
            if urlsplit(src).path.endswith((".jpg", ".jpeg", ".png")):
                errors.append(f"{path}: image has no WebP version: {src}")
        if "images.unsplash.com" in src and "fm=webp" not in src:
            errors.append(f"{path}: unoptimized remote image")
    links = set()
    for href in page.links:
        target = urlsplit(urljoin(url, href))
        if target.netloc == origin and target.scheme in ("http", "https"):
            links.add(target.path or "/")
    graph[path] = links
    pages.append({"path": path, "status": response.status, "h1": " ".join(page.headings[0].split()) if page.headings else "", "html_ms": elapsed_ms})

visited, queue = set(), deque(["/", "/en"])
while queue:
    path = queue.popleft()
    if path in visited:
        continue
    visited.add(path)
    queue.extend(graph.get(path, set()) - visited)
for path in sorted(paths - visited):
    errors.append(f"{path}: orphan page")
for path in sorted(set().union(*graph.values()) - paths):
    if path.startswith("/Admin") or path in ("/sitemap.xml",):
        continue
    response = fetch(base + path, allow_redirects=False)
    if response.status >= 400:
        errors.append(f"broken internal link: {path} HTTP {response.status}")
missing = fetch(base + "/page-that-does-not-exist")
if missing.status != 404:
    errors.append(f"missing page returns {missing.status} instead of 404")
report = {"base": base, "pages": pages, "checked_assets": len(checked_assets), "errors": errors, "scope": "Local HTTP, server HTML, links and structured data. Not field LCP, CLS or INP."}
Path(".preview").mkdir(exist_ok=True)
Path(".preview/seo-verification.json").write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding="utf-8")
print(json.dumps({"pages": len(pages), "checked_assets": len(checked_assets), "errors": len(errors), "sample_errors": errors[:12]}, ensure_ascii=False, indent=2))
sys.exit(1 if errors else 0)
