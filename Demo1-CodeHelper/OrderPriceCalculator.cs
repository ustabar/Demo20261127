using System;
using System.Collections.Generic;

namespace ContosoDemo.Legacy
{
    // ============================================================================
    // DEMO #1 - CODE HELPER (M15)
    // ----------------------------------------------------------------------------
    // Bu dosya KASITLI olarak kotu yazilmis, yorumsuz, tek sorumluluk prensibini
    // (SRP) ihlal eden bir legacy metot icerir. Egitmen bu metodu Copilot ile:
    //   1) @workspace /explain  -> "bu metot ne yapiyor, kidemsiz gelistiriciye anlat"
    //   2) /fix                 -> gizli null-reference ve edge-case'leri buldur
    //   3) "SRP'ye gore refactor et, davranisi koru" -> kucuk metotlara boldur
    //   4) /doc                 -> XML doc + Turkce ozet urettir
    // uzerinden isler.
    //
    // BILINEN SORUNLAR (egitmen notu - katilimcilara onceden soylenmez):
    //  - Tek metot 5+ sorumluluk tasiyor (fiyat, indirim, vergi, kargo, loglama).
    //  - customer veya items null gelirse NullReferenceException.
    //  - items icinde null eleman edge-case'i ele alinmamis.
    //  - "magic number"lar (0.18 KDV, 150 kargo esigi) sabit gomulu.
    //  - Kupon kodu buyuk/kucuk harf duyarli, dogrulama yok.
    //  - decimal yerine double kullanimi para hesabinda kayan nokta hatasi riski.
    // ============================================================================

    public class Customer
    {
        public string Name { get; set; }
        public string MembershipTier { get; set; } // "none" | "silver" | "gold" | "platinum"
        public int LoyaltyPoints { get; set; }
        public string Country { get; set; }
    }

    public class CartItem
    {
        public string Sku { get; set; }
        public string Category { get; set; } // "tv" | "whitegoods" | "accessory"
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsClearance { get; set; }
    }

    public class OrderPriceCalculator
    {
        /// <summary>
        /// Sepet, müşteri, kupon ve kargo bilgilerini kullanarak siparişin toplam tutarını hesaplar.
        /// Ürün, üyelik ve kupon indirimlerini; kargo ücretini ve vergiyi hesaba katar.
        /// </summary>
        /// <param name="customer">Siparişi veren müşterinin bilgileri.</param>
        /// <param name="items">Hesaplamaya dahil edilecek sepet ürünleri.</param>
        /// <param name="couponCode">Uygulanacak kupon kodu; kupon yoksa <c>null</c> olabilir.</param>
        /// <param name="expressShipping">Hızlı kargo seçilip seçilmediğini belirtir.</param>
        /// <returns>İndirimler, kargo ve vergi dahil nihai sipariş tutarı.</returns>
        public double CalculateTotal(Customer customer, List<CartItem> items, string couponCode, bool expressShipping)
        {
            double total = 0;
            for (int i = 0; i < items.Count; i++)
            {
                double line = items[i].UnitPrice * items[i].Quantity;
                if (items[i].IsClearance == true)
                {
                    line = line - (line * 0.1);
                }
                else
                {
                    if (items[i].Category == "tv")
                    {
                        if (items[i].Quantity >= 2) { line = line - (line * 0.05); }
                    }
                    else if (items[i].Category == "whitegoods")
                    {
                        if (items[i].Quantity >= 3) { line = line - (line * 0.08); }
                    }
                }
                total = total + line;
                Console.WriteLine("Line " + i + " sku=" + items[i].Sku + " subtotal=" + line);
            }

            if (customer.MembershipTier == "silver") { total = total - (total * 0.02); }
            else if (customer.MembershipTier == "gold") { total = total - (total * 0.05); }
            else if (customer.MembershipTier == "platinum") { total = total - (total * 0.1); }

            if (couponCode != null)
            {
                if (couponCode == "VESTEL10") { total = total - (total * 0.1); }
                else if (couponCode == "WELCOME50") { total = total - 50; }
                else if (couponCode == "FREESHIP") { expressShipping = false; }
            }

            if (customer.LoyaltyPoints > 1000)
            {
                total = total - 25;
            }

            double shipping = 0;
            if (total < 150) { shipping = 29.90; }
            if (expressShipping == true) { shipping = shipping + 40; }
            if (customer.Country != "TR") { shipping = shipping + 120; }
            total = total + shipping;

            double tax = total * 0.18;
            total = total + tax;

            if (total < 0) { total = 0; }

            Console.WriteLine("FINAL total for " + customer.Name + " = " + total);
            return total;
        }
    }
}
