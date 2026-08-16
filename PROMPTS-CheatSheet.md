# Copilot Prompt Şablonları — Cheat Sheet

Eğitimde ve sonrasında kullanılabilecek, yeniden kullanılabilir prompt şablonları.
Yapı: **Rol + Amaç + Kısıt + Çıktı Formatı**.

---

## 1. Açıklama (Explain)

```
Sen kıdemli bir yazılım mimarısın. Aşağıdaki kodu, konuya yeni başlamış bir
geliştiriciye anlatır gibi açıkla. Ne yaptığını, neden böyle yazıldığını ve
olası risklerini madde madde belirt. Kısa ve net ol.
```

## 2. Refactor

```
Sen kıdemli bir .NET geliştiricisisin. Bu metodu Single Responsibility Principle'a
göre daha küçük, tek sorumluluklu metotlara böl.
KISIT: Mevcut davranışı ve public imzayı KORU.
ÇIKTI: Refactor edilmiş kod + kısa bir "neyi neden değiştirdim" özeti.
```

## 3. Hata Bulma / Düzeltme (Fix)

```
Bu kodda gizli hatalar, null-reference riskleri ve ele alınmamış edge-case'ler var mı?
Bul, önem sırasına göre listele ve her biri için düzeltme öner.
ÇIKTI: Tablo (Sorun | Risk | Önerilen düzeltme).
```

## 4. Güvenlik İncelemesi (Security Review)

```
Sen kıdemli bir uygulama güvenliği uzmanısın. Bu dosyayı OWASP Top 10 açısından incele.
Her bulgu için: (1) açığın adı ve OWASP kategorisi, (2) risk açıklaması, (3) severity
(Kritik/Yüksek/Orta/Düşük), (4) güvenli alternatif kod örneği.
ÇIKTI: Bulgu başına başlıklı bölümler.
```

## 5. Test Üretimi

```
Bu sınıf için xUnit birim testleri üret.
KAPSAM: happy path + sınır (edge) durumları + exception senaryoları.
KISIT: Test isimleri MetotAdi_Kosul_BeklenenSonuc deseninde olsun.
ÇIKTI: Derlenebilir test sınıfı + hangi senaryoların kapsandığının kısa listesi.
```

## 6. Dokümantasyon

```
Bu metoda XML documentation comment ekle (summary, param, returns, exceptions).
Ayrıca metodun ne yaptığını 2-3 cümlelik Türkçe bir özetle açıkla.
```

## 7. Spesifikasyondan Kod (Protokol/API)

```
Ekteki spesifikasyonu (#file) temel al. Bu formata göre tipli bir parser ve builder
sınıfı üret. Alanları tipli property'lere maple, doğrulama kurallarını uygula.
KISIT: Round-trip (Parse -> Build) orijinal mesajı birebir üretmeli.
ÇIKTI: Parser + builder + örnek mesajın parse doğrulaması + xUnit testleri.
```

## 8. Mimari Analiz (MindMap)

```
@workspace Bu projenin katmanlı mimarisini çıkar. Modülleri, sorumluluklarını ve
bağımlılıklarını bir Mermaid graph/mindmap olarak üret. Ardından teknik borç ve
riskli alanları (yüksek coupling, test eksikliği, TODO'lar) işaretle.
```

## 9. Görselden Kod

```
Ekteki mockup görselini temel al. Bu tasarımı responsive bir React + Tailwind
bileşenine çevir. Erişilebilirlik (a11y) temel kurallarına uy.
ÇIKTI: Tek dosyalık bileşen + kullanılan varsayımların kısa listesi.
```

## 10. AI Chain (Çoklu Model İş Akışı)

Ayrı ayrı, sırayla:
```
[1/4 PLAN]   Reasoning model: "Şu özelliği ekleyeceğim: ... . Adım adım bir uygulama planı çıkar."
[2/4 KOD]    Hızlı model:     "Bu plana göre iskeleti ve endpoint'leri üret."
[3/4 REVIEW] "Bu değişikliği kıdemli bir reviewer gibi incele; riskleri ve kod kokularını bul."
[4/4 COMMIT] "Bu diff için Conventional Commits formatında Türkçe bir commit mesajı yaz."
```

---

### İYİ vs KÖTÜ prompt

| ❌ Kötü        | ✅ İyi |
|----------------|--------|
| "bunu düzelt"  | "Kıdemli mimar olarak bu metodu SRP'ye göre böl; public imzayı koru; sadece değişen metotları ve kısa gerekçeyi ver." |
| "test yaz"     | "Bu servise xUnit ile happy + edge + exception testleri üret; isimlendirme MetotAdi_Kosul_BeklenenSonuc." |
| "güvenli mi?"  | "OWASP Top 10 açısından incele; her bulgu için risk, severity ve güvenli alternatif kod ver." |
