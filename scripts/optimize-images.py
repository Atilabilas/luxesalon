"""Create WebP and responsive variants; retain original images and CMS URLs."""
import json
from pathlib import Path
from PIL import Image, ImageOps

root = Path(__file__).resolve().parents[1] / "LuxeSalon.Web" / "wwwroot"
manifest, original_bytes, optimized_bytes = {}, 0, 0
for source in sorted((root / "images").rglob("*")):
    if source.suffix.lower() not in (".jpg", ".jpeg", ".png"):
        continue
    with Image.open(source) as image:
        image = ImageOps.exif_transpose(image)
        image = image.convert("RGBA" if "A" in image.getbands() else "RGB")
        output = source.with_suffix(".webp")
        image.save(output, "WEBP", quality=80, method=6)
        src = "/" + output.relative_to(root).as_posix()
        variants = []
        for width in (480, 800):
            if width >= image.width:
                continue
            height = round(image.height * width / image.width)
            variant = output.with_name(f"{output.stem}.{width}.webp")
            image.resize((width, height), Image.Resampling.LANCZOS).save(variant, "WEBP", quality=78, method=6)
            variants.append(f"/{variant.relative_to(root).as_posix()} {width}w")
        variants.append(f"{src} {image.width}w")
        asset = {"src": src, "srcset": ", ".join(variants), "width": image.width, "height": image.height}
        manifest["/" + source.relative_to(root).as_posix()] = asset
        manifest[src] = asset
        original_bytes += source.stat().st_size
        optimized_bytes += output.stat().st_size
(root / "images" / "image-manifest.json").write_text(json.dumps(manifest, indent=2), encoding="utf-8")
print(json.dumps({"images": len(manifest) // 2, "original_bytes": original_bytes, "webp_bytes": optimized_bytes, "saved_percent": round(100 * (1 - optimized_bytes / original_bytes), 1)}))
