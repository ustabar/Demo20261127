import unittest

from claims_intake import submit_claim


class Claim218Tests(unittest.TestCase):
    def test_rejects_missing_required_field(self):
        self.assertEqual(
            submit_claim({"claim_ref": "DEMO-218"}),
            (400, {"error": "missing_required_fields"}),
        )

    def test_rejects_blank_or_non_string_fields(self):
        for invalid in ("", "   ", 218, None):
            with self.subTest(invalid=invalid):
                self.assertEqual(
                    submit_claim({"claim_ref": invalid, "loss_date": "2026-01-15"}),
                    (400, {"error": "missing_required_fields"}),
                )
