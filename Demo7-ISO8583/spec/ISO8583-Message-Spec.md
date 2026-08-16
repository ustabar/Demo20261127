# ISO 8583 Benzeri Mesaj Formatı Spesifikasyonu (Demo #7)

> Bu spesifikasyon, **Demo #7 — Doküman ve Protokol Analizi (M21)** için basitleştirilmiş,
> eğitim amaçlı bir ISO 8583 türevidir. Gerçek ISO 8583 standardının tam kopyası değildir;
> Copilot'un bir spesifikasyondan tipli parser/builder kodu üretme yeteneğini göstermek için
> tasarlanmıştır.
>
> **Eğitmen akışı:** Bu `.md` dosyasını Copilot Chat bağlamına ekle (`#file`) ve şu prompt'u ver:
> *"Bu spesifikasyona göre C# ile bir ISO 8583 mesaj parser ve builder sınıfı üret. Her alanı
> tipli property'lere maple, bitmap'i doğru yorumla, örnek ham mesajı parse et ve doğrula.
> Kapsamlı xUnit testleri de ekle."*

***

## 1. Mesaj Yapısı

Bir mesaj sırayla şu bölümlerden oluşur:

```
[ MTI (4 hane) ] [ Primary Bitmap (16 hex = 64 bit) ] [ Data Elements... ]
```

* Tüm alanlar **ASCII** kodlanmıştır.
* Sayısal uzunluk ön ekleri (LLVAR/LLLVAR) ASCII rakamdır.

## 2. MTI (Message Type Indicator)

4 haneli sayısal. Örnekler:

| MTI  | Anlam                             |
| ---- | --------------------------------- |
| 0200 | Finansal istek (request)          |
| 0210 | Finansal istek yanıtı (response)  |
| 0400 | İptal (reversal) isteği           |
| 0410 | İptal yanıtı                      |
| 0800 | Ağ yönetimi isteği (echo/sign-on) |
| 0810 | Ağ yönetimi yanıtı                |

## 3. Primary Bitmap

* **16 hex karakter** (64 bit). MTI'den hemen sonra gelir.
* Soldan sağa bit 1..64. Bit **N set (1)** ise, **Data Element N** mesajda vardır.
* Bit 1, ikincil bitmap varlığını gösterir (bu basitleştirilmiş sürümde **her zaman 0**, yalnızca birincil bitmap kullanılır).

Örnek: bitmap `7220000000000000`

* Hex `7` = `0111` → bit 2,3,4 set
* Hex `2` = `0010` → bit 7 set
* Hex `2` = `0010` → bit 11 set
* Kalan `0` → başka alan yok
* Yani mevcut alanlar: **DE2, DE3, DE4, DE7, DE11**

## 4. Data Element (Alan) Tanımları

| DE | Alan Adı                     | Tip     | Uzunluk / Format                                    |
| -- | ---------------------------- | ------- | --------------------------------------------------- |
| 2  | Primary Account Number (PAN) | LLVAR n | 2 haneli uzunluk öneki + 1..19 rakam                |
| 3  | Processing Code              | n       | Sabit 6 hane                                        |
| 4  | Transaction Amount           | n       | Sabit 12 hane (kuruş; sağdan hizalı, sıfır dolgulu) |
| 7  | Transmission Date & Time     | n       | Sabit 10 hane (MMDDhhmmss)                          |
| 11 | System Trace Audit Number    | n       | Sabit 6 hane                                        |
| 12 | Local Transaction Time       | n       | Sabit 6 hane (hhmmss)                               |
| 13 | Local Transaction Date       | n       | Sabit 4 hane (MMDD)                                 |
| 37 | Retrieval Reference Number   | an      | Sabit 12 karakter                                   |
| 39 | Response Code                | an      | Sabit 2 karakter (yalnız yanıt mesajları)           |
| 41 | Card Acceptor Terminal ID    | ans     | Sabit 8 karakter                                    |
| 49 | Currency Code                | n       | Sabit 3 hane (ISO 4217, örn. 949 = TRY)             |

**Tip kısaltmaları:** `n` = sayısal, `an` = alfanümerik, `ans` = alfanümerik + özel karakter.
**LLVAR:** değişken uzunluk; 2 haneli ASCII uzunluk öneki ardından veri.

## 5. Kurallar ve Doğrulama

1. MTI 4 haneli sayısal olmalı.
2. Bitmap tam 16 hex karakter olmalı; geçerli hex.
3. Bitmap'te set olan **her** bit için ilgili DE mevcut ve sırayla parse edilmeli.
4. Sabit uzunluklu alanlar tam uzunlukta olmalı; eksikse hata.
5. DE4 (tutar) kuruş cinsindendir: `000000150000` = 1500.00 birim.
6. DE39 yalnızca yanıt (xx10) mesajlarında bulunur; istek mesajında bit 39 set olmamalı.
7. Bilinmeyen/desteklenmeyen DE → `UnsupportedFieldException`.

## 6. Örnek Ham Mesaj (parse edilecek)

```
0200722000000000000016451234567890123456000000000000015000010212303045123456
```

Çözümlemesi:

| Bölüm       | Değer                         | Açıklama                        |
| ----------- | ----------------------------- | ------------------------------- |
| MTI         | `0200`                        | Finansal istek                  |
| Bitmap      | `7220000000000000`            | DE2, DE3, DE4, DE7, DE11 mevcut |
| DE2 (LLVAR) | len `16` → `4512345678901234` | 16 haneli PAN                   |
| DE3         | `560000`? → *aşağıya bak*     | Processing code (6 hane)        |

> **Not (eğitmen):** Yukarıdaki ham örnek, katılımcıların ve Copilot'un birlikte
> çözümlemesi için bilinçli olarak "elle takip edilecek" uzunluktadır. Copilot'tan
> önce parser'ı üretmesini, sonra bu mesajı parse edip alan alan doğrulamasını isteyin.
> Copilot'un çıktısını spesifikasyona göre **birlikte teyit edin** — sadakat kontrolü
> bu demonun ana mesajıdır.

## 7. Beklenen Parser API (öneri)

Copilot'a şu şekli hedeflemesini önerebilirsiniz:

```csharp
var message = Iso8583Parser.Parse(rawMessage);
string mti = message.Mti;                       // "0200"
bool hasPan = message.HasField(2);              // true
string pan = message.GetString(2);              // "4512345678901234"
decimal amount = message.GetAmount(4);          // 1500.00m
// ...
string rebuilt = Iso8583Builder.Build(message); // rawMessage ile eşleşmeli (round-trip)
```

