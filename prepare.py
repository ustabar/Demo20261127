import argparse
import shutil
from pathlib import Path


ROOT = Path(__file__).resolve().parent
STATES = {
    "baseline": {},
    "review-failing": {"review": ("tests/test_requirements.py",)},
    "review-fixed": {
        "review": ("tests/test_requirements.py",),
        "review-fix": ("claims_intake.py",),
    },
    "security-open": {},
    "security-fixed": {"security-fix": ("audit_marker.py",)},
}


def main() -> None:
    parser = argparse.ArgumentParser(description="Prepare an independent local demo directory")
    parser.add_argument("state", choices=STATES)
    parser.add_argument("destination", type=Path)
    args = parser.parse_args()
    target = args.destination.resolve()
    if target.exists() or target.is_relative_to(ROOT / "sample") or target.is_relative_to(ROOT / "overlays"):
        parser.error("Choose a new destination outside the sample and overlays folders.")
    shutil.copytree(ROOT / "sample", target, ignore=shutil.ignore_patterns("__pycache__", "*.pyc", "*.sarif"))
    for folder, files in STATES[args.state].items():
        for filename in files:
            shutil.copy2(ROOT / "overlays" / folder / filename, target / filename)
    print(f"Prepared {args.state}: {target}")


if __name__ == "__main__":
    main()
