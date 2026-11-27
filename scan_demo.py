import argparse
import ast
import json
from pathlib import Path

RULE = "DEMO001"
MESSAGE = "The synthetic, payload-free audit marker is disabled."


def marker_location(source: Path) -> int | None:
    tree = ast.parse(source.read_text(encoding="utf-8"), filename=str(source))
    matches = [
        node for node in tree.body
        if isinstance(node, ast.Assign) and any(
            isinstance(target, ast.Name) and target.id == "AUDIT_MARKER_ENABLED"
            for target in node.targets
        )
    ]
    if len(matches) != 1 or not isinstance(matches[0].value, ast.Constant):
        raise ValueError("Expected one literal AUDIT_MARKER_ENABLED assignment")
    marker = matches[0]
    if type(marker.value.value) is not bool:
        raise ValueError("AUDIT_MARKER_ENABLED must be a boolean")
    return marker.lineno if not marker.value.value else None


def sarif(lineno: int | None) -> dict:
    results = []
    if lineno is not None:
        results.append({
            "ruleId": RULE,
            "level": "warning",
            "message": {"text": MESSAGE},
            "locations": [{"physicalLocation": {
                "artifactLocation": {"uri": "audit_marker.py"},
                "region": {"startLine": lineno},
            }}],
        })
    return {
        "$schema": "https://json.schemastore.org/sarif-2.1.0.json",
        "version": "2.1.0",
        "runs": [{
            "tool": {"driver": {"name": "Claims Demo Policy", "rules": [{
                "id": RULE, "name": "SyntheticAuditMarker",
                "shortDescription": {"text": MESSAGE},
                "fullDescription": {"text": "Training-only policy for a dummy event with no payload."},
            }]}},
            "results": results,
        }],
    }


def main() -> None:
    parser = argparse.ArgumentParser(description="Scan for the one training-only audit rule")
    parser.add_argument("--sarif", type=Path, help="Write SARIF as well as printing the result")
    args = parser.parse_args()
    line = marker_location(Path(__file__).with_name("audit_marker.py"))
    if args.sarif is not None:
        args.sarif.write_text(json.dumps(sarif(line), indent=2) + "\n", encoding="utf-8")
    print(f"audit_marker.py:{line}: {RULE}: {MESSAGE}" if line else "0 findings (DEMO001)")


if __name__ == "__main__":
    main()
