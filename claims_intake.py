import json
import sys


def submit_claim(summary: object) -> tuple[int, dict[str, str]]:
    if not isinstance(summary, dict):
        return 400, {"error": "invalid_submission"}
    if any(
        not isinstance(value, str) or not value.strip()
        for value in (summary.get(field) for field in ("claim_ref", "loss_date"))
    ):
        return 400, {"error": "missing_required_fields"}
    return 202, {"status": "accepted"}


if __name__ == "__main__":
    status, body = submit_claim(json.load(sys.stdin))
    print(json.dumps({"status_code": status, **body}))
