using System;
using System.Collections.Generic;

namespace ContosoDemo.Iso8583
{
    // ============================================================================
    // DEMO #7 - ISO 8583 PARSER ISKELETI (M21)
    // ----------------------------------------------------------------------------
    // Bu dosya BILINCLI olarak bir "baslangic iskeleti"dir. Egitmen, spec/
    // ISO8583-Message-Spec.md dosyasini Copilot baglamina ekleyip su prompt'u verir:
    //
    //   "spec/ISO8583-Message-Spec.md dosyasindaki spesifikasyona gore asagidaki
    //    TODO'lari doldur. Parse ve Build metotlarini tamamla, bitmap yorumlamasini
    //    ve alan tiplerini spec'e sadik uygula. Round-trip (Parse -> Build) ayni ham
    //    mesaji uretmeli. Sonra ornek ham mesaji parse edip alanlari dogrula."
    //
    // Amac: Copilot'un bir spesifikasyondan tipli, dogru protokol kodu uretmesini
    // canli gostermektir. Uretilen kod spec'e gore ELLE teyit edilmelidir.
    // ============================================================================

    /// <summary>Cozumlenmis bir ISO 8583 mesajini temsil eder.</summary>
    public class Iso8583Message
    {
        public string Mti { get; set; }
        private readonly Dictionary<int, string> _fields = new Dictionary<int, string>();

        public bool HasField(int de) => _fields.ContainsKey(de);
        public string GetString(int de) => _fields.TryGetValue(de, out var v) ? v : null;
        public void SetString(int de, string value) => _fields[de] = value;

        /// <summary>DE4 gibi tutar alanlarini kurus -> ondalik birime cevirir.</summary>
        public decimal GetAmount(int de)
        {
            // TODO (Copilot): _fields[de] icindeki 12 haneli kurus degerini
            // decimal'e cevir (orn. "000000150000" -> 1500.00m). Spec bolum 4 & 5.
            throw new NotImplementedException("Copilot ile doldurulacak.");
        }

        public IReadOnlyDictionary<int, string> Fields => _fields;
    }

    public class Iso8583Exception : Exception
    {
        public Iso8583Exception(string message) : base(message) { }
    }

    public class UnsupportedFieldException : Iso8583Exception
    {
        public UnsupportedFieldException(int de) : base($"Desteklenmeyen alan: DE{de}") { }
    }

    public static class Iso8583Parser
    {
        /// <summary>Ham ASCII mesaji cozumleyip Iso8583Message dondurur.</summary>
        public static Iso8583Message Parse(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                throw new Iso8583Exception("Bos mesaj.");

            // TODO (Copilot): Asagidaki adimlari spec'e gore uygula.
            // 1) Ilk 4 karakter = MTI (sayisal dogrula).
            // 2) Sonraki 16 karakter = primary bitmap (hex dogrula).
            // 3) Bitmap'teki her set bit icin ilgili DE'yi sirayla oku:
            //    - LLVAR alanlar (orn. DE2): 2 haneli uzunluk oneki + veri.
            //    - Sabit uzunluklu alanlar: spec tablosundaki uzunluk kadar oku.
            // 4) Bilinmeyen DE -> UnsupportedFieldException.
            // 5) Dolu Iso8583Message dondur.
            throw new NotImplementedException("Copilot ile doldurulacak. Bkz: spec/ISO8583-Message-Spec.md");
        }
    }

    public static class Iso8583Builder
    {
        /// <summary>Iso8583Message nesnesini tekrar ham ASCII mesaja cevirir (round-trip).</summary>
        public static string Build(Iso8583Message message)
        {
            // TODO (Copilot): Parse'in tersini yap.
            // 1) MTI yaz.
            // 2) Mevcut alanlardan bitmap'i olustur (16 hex).
            // 3) Alanlari DE sirasina gore, dogru format/uzunlukla yaz.
            // 4) Sonuc, ayni mesajin Parse ciktisiyla round-trip uyumlu olmali.
            throw new NotImplementedException("Copilot ile doldurulacak.");
        }
    }

    // Egitmen: asagidaki alan tablosunu Copilot'a referans olarak gosterebilirsiniz.
    // Gercek uygulamada bu tablo spec'ten uretilir.
    public static class FieldCatalog
    {
        // DE -> (Ad, Tip, Sabit uzunluk | -1 = LLVAR)
        public static readonly Dictionary<int, (string Name, string Type, int Length)> Definitions =
            new Dictionary<int, (string, string, int)>
            {
                { 2,  ("PAN",             "LLVAR-n", -1) },
                { 3,  ("Processing Code", "n",        6) },
                { 4,  ("Amount",          "n",       12) },
                { 7,  ("Transmission DT", "n",       10) },
                { 11, ("STAN",            "n",        6) },
                { 12, ("Local Time",      "n",        6) },
                { 13, ("Local Date",      "n",        4) },
                { 37, ("RRN",             "an",      12) },
                { 39, ("Response Code",   "an",       2) },
                { 41, ("Terminal ID",     "ans",      8) },
                { 49, ("Currency Code",   "n",        3) },
            };
    }
}
