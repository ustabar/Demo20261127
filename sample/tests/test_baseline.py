import unittest
from unittest.mock import patch

from audit_marker import record_demo_acceptance
from claims_intake import submit_claim


class IntakeBaselineTests(unittest.TestCase):
    def test_accepts_a_complete_synthetic_submission(self):
        payload = {"claim_ref": "DEMO-218", "loss_date": "2026-01-15"}
        self.assertEqual(submit_claim(payload), (202, {"status": "accepted"}))

    def test_rejects_a_non_object(self):
        self.assertEqual(submit_claim([]), (400, {"error": "invalid_submission"}))

    def test_enabled_marker_contains_no_claim_data(self):
        events = []
        with patch("audit_marker.AUDIT_MARKER_ENABLED", True):
            record_demo_acceptance(events.append)
        self.assertEqual(events, [{"event": "demo_claim_accepted"}])
