using System;

namespace ContosoDemo.Pricing
{
    // ============================================================================
    // DEMO #4 - TEST CASE & UI TEST URETIMI (M18)
    // ----------------------------------------------------------------------------
    // Bu sinif KASITLI olarak TEMIZ ama TESTSIZ birakilmistir. Egitmen Copilot'a:
    //   1) /tests -> "bu sinif icin xUnit birim testleri uret: happy path + edge +
    //                 exception. Isimlendirme MetotAdi_Kosul_BeklenenSonuc."
    //   2) "Eksik coverage alanlarini bul" -> test edilmeyen dallari isaretlet.
    //   3) Uretilen testleri CALISTIR, yesil/kirmizi goster.
    //
    // Test edilmesi gereken dallar (egitmen notu):
    //   - Gecersiz miktar (<= 0) -> ArgumentException
    //   - Bilinmeyen musteri seviyesi -> varsayilan %0 indirim
    //   - Kupon: null / gecerli / gecersiz kod
    //   - Sinir degerler: tam 1000 sadakat puani, tam 150 esik
    //   - Negatif sonuc asla dondurulmez (0'a sabitlenir)
    // ============================================================================

    public enum MembershipTier { None, Silver, Gold, Platinum }

    public class DiscountResult
    {
        public decimal FinalAmount { get; set; }
        public decimal DiscountApplied { get; set; }
        public string AppliedCoupon { get; set; }
    }

    public class DiscountService
    {
        public DiscountResult ApplyDiscount(decimal baseAmount, int quantity,
            MembershipTier tier, string couponCode, int loyaltyPoints)
        {
            if (quantity <= 0)
                throw new ArgumentException("Miktar 0'dan büyük olmalı.", nameof(quantity));
            if (baseAmount < 0)
                throw new ArgumentException("Tutar negatif olamaz.", nameof(baseAmount));

            decimal amount = baseAmount;
            decimal originalAmount = amount;

            // Uyelik seviyesine gore indirim
            decimal tierRate = tier switch
            {
                MembershipTier.Silver => 0.02m,
                MembershipTier.Gold => 0.05m,
                MembershipTier.Platinum => 0.10m,
                _ => 0m
            };
            amount -= amount * tierRate;

            // Kupon
            string applied = null;
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                switch (couponCode.Trim().ToUpperInvariant())
                {
                    case "VESTEL10":
                        amount -= amount * 0.10m; applied = "VESTEL10"; break;
                    case "WELCOME50":
                        amount -= 50m; applied = "WELCOME50"; break;
                }
            }

            // Sadakat puani odulu
            if (loyaltyPoints > 1000)
                amount -= 25m;

            if (amount < 0) amount = 0m;

            return new DiscountResult
            {
                FinalAmount = decimal.Round(amount, 2),
                DiscountApplied = decimal.Round(originalAmount - amount, 2),
                AppliedCoupon = applied
            };
        }
    }
}
