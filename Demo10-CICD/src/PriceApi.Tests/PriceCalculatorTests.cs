using PriceApi;
using Xunit;

namespace PriceApi.Tests;

public class PriceCalculatorTests
{
    [Fact]
    public void ApplyDiscount_NoTier_ReturnsBaseTotal()
    {
        var result = PriceCalculator.ApplyDiscount(100m, 2, Tier.None);
        Assert.Equal(200m, result);
    }

    [Fact]
    public void ApplyDiscount_Gold_Applies5Percent()
    {
        var result = PriceCalculator.ApplyDiscount(100m, 1, Tier.Gold);
        Assert.Equal(95m, result);
    }

    [Fact]
    public void ApplyDiscount_BulkQuantity_AppliesExtra5Percent()
    {
        // 10 adet * 100 = 1000, platinum %10 -> 900, toplu %5 -> 855
        var result = PriceCalculator.ApplyDiscount(100m, 10, Tier.Platinum);
        Assert.Equal(855m, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void ApplyDiscount_InvalidQuantity_Throws(int qty)
    {
        Assert.Throws<ArgumentException>(() => PriceCalculator.ApplyDiscount(100m, qty, Tier.None));
    }

    [Fact]
    public void ApplyDiscount_NegativeAmount_Throws()
    {
        Assert.Throws<ArgumentException>(() => PriceCalculator.ApplyDiscount(-1m, 1, Tier.None));
    }
}
