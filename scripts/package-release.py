"""Package a published IIS application without server configuration or user data."""
import hashlib
import json
import sys
import zipfile
from pathlib import Path

source = Path(sys.argv[1]).resolve()
archive = Path(sys.argv[2]).resolve()
workspace = Path(__file__).resolve().parents[1]
if not source.is_relative_to(workspace) or not archive.is_relative_to(workspace):
    raise SystemExit("Publish source and archive must be inside the workspace")
if not (source / "LuxeSalon.Web.exe").is_file():
    raise SystemExit("The published application was not found")

def excluded(path):
    name = path.name.lower()
    folders = {p.lower() for p in path.parts[:-1]}
    return (name == "web.config" or name.startswith("appsettings") or
            name == "app_offline.htm" or name.endswith((".db", ".db-wal", ".db-shm", ".pdb")) or
            bool(folders & {"app_data", "uploads", "logs", "keys", "dataprotection-keys"}))

files, skipped = [], []
archive.parent.mkdir(parents=True, exist_ok=True)
with zipfile.ZipFile(archive, "w", compression=zipfile.ZIP_DEFLATED, compresslevel=6) as output:
    for file in sorted(source.rglob("*")):
        if not file.is_file():
            continue
        if file.is_symlink():
            raise SystemExit("Symbolic links are not supported in a deployment package")
        relative = file.relative_to(source)
        if excluded(relative):
            skipped.append(relative.as_posix())
            continue
        content = file.read_bytes()
        output.writestr(relative.as_posix(), content)
        files.append({"path": relative.as_posix(), "bytes": len(content), "sha256": hashlib.sha256(content).hexdigest()})

with zipfile.ZipFile(archive) as output:
    forbidden = [name for name in output.namelist() if excluded(Path(name))]
    if forbidden:
        raise SystemExit("Protected files were found in the deployment archive")
    bad_file = output.testzip()
    if bad_file:
        raise SystemExit("Deployment archive integrity check failed")

manifest = {"source": str(source), "archive": str(archive), "archiveBytes": archive.stat().st_size,
            "archiveSha256": hashlib.sha256(archive.read_bytes()).hexdigest(),
            "runtime": "win-x86 self-contained", "excluded": skipped, "files": files,
            "protected": "Never upload or modify existing appsettings*, web.config, App_Data, uploads, logs or keys"}
archive.with_suffix(".manifest.json").write_text(json.dumps(manifest, indent=2), encoding="utf-8")
print(json.dumps({"archive": str(archive), "files": len(files), "archiveBytes": manifest["archiveBytes"],
                  "excluded": skipped, "integrity": "passed"}, indent=2))
