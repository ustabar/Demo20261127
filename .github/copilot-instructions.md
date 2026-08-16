# GitHub Copilot Repository Instructions — Contoso Demo

> **Demo amaçlı örnek.** Bu dosya, **M13 — Repository Instructions** modülünde canlı
> gösterilmek üzere hazırlanmıştır. Copilot, bu repo'da öneri üretirken aşağıdaki
> kuralları otomatik olarak dikkate alır. Eğitmen, bu dosyayı düzenleyip önerilerin
> nasıl değiştiğini canlı gösterebilir.

## Genel

- Bu bir **.NET / C#** ve yer yer **TypeScript/React** kod tabanıdır.
- Hedef framework: **.NET 8**, dil sürümü **C# 12**.
- Kod yorumları ve commit mesajları **Türkçe** yazılır; genel teknik terimler İngilizce kalabilir.

## Kodlama Standartları

- **Repository pattern** ve **dependency injection** kullan; statik servis/singleton'dan kaçın.
- Para/tutar hesaplarında **`decimal`** kullan, asla `double` değil.
- Public API imzalarını gereksiz yere değiştirme; geriye dönük uyumluluğu koru.
- Null güvenliği: nullable reference types açık; giriş parametrelerini doğrula (guard clauses).
- Sihirli sayı/string kullanma; sabitleri anlamlı `const`/`enum` olarak tanımla.
- Loglama için **Serilog** kullan; hassas veriyi (şifre, PAN, token) **asla loglama**.

## Test

- Birim testleri **xUnit** ile yaz.
- Her yeni public metot için happy path + edge-case + exception testleri ekle.
- Test isimleri: `MetotAdi_Kosul_BeklenenSonuc` deseninde.

## Güvenlik

- SQL için **parametreli sorgu** veya EF Core kullan; string birleştirme ile sorgu kurma.
- Sırlar (connection string, API key) **koda gömülmez**; `IConfiguration` / secret manager.
- Parola hash'leme için **bcrypt/Argon2**; MD5/SHA1 kullanma.

## İsimlendirme

- Sınıf/metot: `PascalCase`, yerel değişken/parametre: `camelCase`, private alan: `_camelCase`.
- Async metotlar `Async` son ekiyle biter.

---

**Eğitmen notu:** Bu kuralları koyup Copilot'a örneğin *"bu servise bir ödeme metodu ekle"*
dediğinizde; Copilot `decimal` kullanır, parametreli sorgu üretir, Türkçe yorum yazar ve
xUnit testi önerir. Sonra bir kuralı değiştirip (örn. testi NUnit yapın) aynı prompt'u
tekrar deneyip önerinin değiştiğini gösterin.
