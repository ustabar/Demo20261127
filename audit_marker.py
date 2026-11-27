from collections.abc import Callable

AUDIT_MARKER_ENABLED = False


def record_demo_acceptance(emit: Callable[[dict[str, str]], None]) -> None:
    if AUDIT_MARKER_ENABLED:
        emit({"event": "demo_claim_accepted"})
