# Demo #10 — PR, CR ve CI/CD Süreçlerinde Copilot (M14)

> **Amaç:** GitHub Copilot'un yazılım teslim hattının (CI/CD) her adımında nasıl
> yardımcı olduğunu canlı göstermek: workflow üretimi/düzeltme, PR açıklaması,
> code review ve başarısız pipeline analizi.
> **Eğitim modülü:** M14 (PR/CR/CI-CD) + M18 (Test) + M16 (Security) ile bağlantılı.

---

## Klasör Yapısı

```
Demo10-CICD/
├── src/
│   ├── PriceApi/            # Küçük iş mantığı (CI'da build+test edilecek)
│   │   ├── PriceApi.csproj
│   │   └── PriceCalculator.cs
│   └── PriceApi.Tests/      # xUnit testleri
│       ├── PriceApi.Tests.csproj
│       └── PriceCalculatorTests.cs
├── starter/.github/workflows/ci.yml   # KASITLI BOZUK workflow (Copilot düzeltir)
├── ANSWER-KEY/.github/                 # Cevap anahtarı: doğru ci.yml, cd.yml, PR template
├── logs/failing-pipeline.log           # Analiz edilecek başarısız pipeline logu
├── prompts.md                          # Kopyala-yapıştır komutlar
└── INSTRUCTOR-NOTES.md
```

> Not: `starter` içindeki workflow bilinçli olarak hatalıdır. `ANSWER-KEY` gerçek
> çözümleri içerir; katılımcılara demo bitince gösterilir.

---

## Demo Akışı (~15 dk)

### Bölüm 1 — CI workflow üretimi (4 dk)
1. `prompts.md` #1: sıfırdan `.github/workflows/ci.yml` ürettir.
2. Copilot'un ürettiğini `ANSWER-KEY/.github/workflows/ci.yml` ile karşılaştır.

### Bölüm 2 — Bozuk workflow'u düzelt (4 dk)
1. `starter/.github/workflows/ci.yml` dosyasını aç.
2. `prompts.md` #2: hataları buldurup düzelttir.
3. **Beklenen bulgular:** `on:` iki nokta eksik, `chekout@v1` yazım+eski sürüm,
   `dotnet-version` eksik, proje yolu (`src/`) yanlış, Test adımında `uses` yerine
   `run` olmalı, adım sırası (restore→build→test).

### Bölüm 3 — PR açıklaması + Code Review (4 dk)
1. `prompts.md` #4: PR şablonuna göre açıklama ürettir.
2. `prompts.md` #5: `/review` ile (veya çoklu model) değişiklikleri incelet.
3. **Vurgu:** Copilot PR özeti/review önerir; **onay insanda** (M14 kalite kapısı).

### Bölüm 4 — Başarısız pipeline analizi (3 dk)
1. `logs/failing-pipeline.log` içeriğini Chat'e yapıştır (`prompts.md` #6).
2. Copilot'un kök nedeni (toplu indirim mantığı + CS0162 unreachable) bulmasını
   ve kodun hatalı, testin doğru olduğunu söylemesini bekle.

> Zaman azsa: Bölüm 2 + Bölüm 4 en yüksek etkiyi verir.

---

## Yerel Doğrulama (eğitmen ön hazırlığı)

Demo öncesi kodun ve testlerin çalıştığını doğrula:
```
cd src
dotnet test PriceApi.Tests/PriceApi.Tests.csproj
```
(5 testin tamamı yeşil olmalı. `failing-pipeline.log` gerçek bir çalıştırma değil,
demo amaçlı örnek bir çıktıdır.)

---

## Eğitmen Vurguları

- Copilot, YAML gibi hataya açık dosyalarda **ciddi zaman kazandırır**; ama üretilen
  workflow **gerçek bir çalıştırmayla doğrulanmalıdır**.
- **Secret'lar asla YAML'a gömülmez** — GitHub Secrets/Variables (CD örneğinde gösterildi).
- Code review'da Copilot bir **ikinci göz**dür; merge kararı ve sorumluluk ekipte kalır.
- Pipeline log analizi, uzun logları **dakikalar içinde** kök nedene indirger.

## Contoso Eğitimindeki Yeri

M14 (PR/CR/CI-CD) modülünün canlı karşılığıdır. Demo #4 (Test) ve Demo #8
(çoklu-model `/review`) ile doğal olarak zincirlenir: kod → test → PR → review → CI → CD.
