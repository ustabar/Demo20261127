namespace PriceApi;

// DEMO #10 - CI/CD
// CI pipeline'inda derlenip test edilecek kucuk is mantigi.
public enum Tier { None, Silver, Gold, Platinum }

public static class PriceCalculator
{
    public static decimal ApplyDiscount(decimal amount, int quantity, Tier tier)
    {
        if (quantity <= 0) throw new ArgumentException("Miktar 0'dan buyuk olmali.", nameof(quantity));
        if (amount < 0) throw new ArgumentException("Tutar negatif olamaz.", nameof(amount));

        decimal total = amount * quantity;

        decimal rate = tier switch
        {
            Tier.Silver => 0.02m,
            Tier.Gold => 0.05m,
            Tier.Platinum => 0.10m,
            _ => 0m
        };
        total -= total * rate;

        // Toplu alim indirimi
        if (quantity >= 10) total -= total * 0.05m;

        return decimal.Round(total, 2);
    }
}
