import json
import sys

REQUIRED_FIELDS = ("claim_ref", "loss_date")


def submit_claim(summary: object) -> tuple[int, dict[str, str]]:
    if not isinstance(summary, dict):
        return 400, {"error": "invalid_submission"}
    for field in REQUIRED_FIELDS:
        value = summary.get(field)
        if not isinstance(value, str) or not value.strip():
            return 400, {"error": "missing_required_fields"}
    return 202, {"status": "accepted"}


if __name__ == "__main__":
    status, body = submit_claim(json.load(sys.stdin))
    print(json.dumps({"status_code": status, **body}))
