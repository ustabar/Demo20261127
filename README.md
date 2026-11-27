# Contoso · GitHub Copilot Eğitimi — Demo Repository

Bu repo, **GitHub Copilot İleri Seviye Geliştirici Eğitimi**ndeki canlı demolar için hazır
örnek kod ve materyalleri içerir. VS Code + GitHub Copilot ile kullanılmak üzere tasarlanmıştır.

> **Not:** Buradaki `.cs` dosyaları bir çözüm (solution) içinde derlenmek için değil, Copilot ile
> **çalışma malzemesi** olarak tasarlanmıştır. Bazı dosyalar kasıtlı olarak hatalı/eksik/güvensizdir
> (demonun amacı budur). İstenirse `dotnet new` ile bir konsol projesine kopyalanıp çalıştırılabilir.

## İçindekiler

| Klasör | Demo | Amaç |
|--------|------|------|
| `Demo1-CodeHelper/` | **Demo #1 — Code Helper** | Karmaşık legacy metodu açıkla / düzelt / refactor / dokümante et |
| `Demo2-SecurityReview/` | **Demo #2 — Security Review** | Güvenlik açıklarını bul + güvenli alternatif üret |
| `Demo3-MindMap/` | **Demo #3 — Proje Analizi** | Katmanlı örnek projeden mimari + teknik borç çıkar |
| `Demo4-TestUI/` | **Demo #4 — Test & UI** | Testsiz servise birim test + React bileşenine UI test üret |
| `Demo5-VibeCoding/` | **Demo #5 — Vibe Coding** | Doğal dille sıfırdan mini uygulama (canlı script) |
| `Demo6-ImageToCode/` | **Demo #6 — Görselden Kod** | Dashboard mockup görselinden React + Tailwind üret |
| `Demo7-ISO8583/` | **Demo #7 — Protokol Analizi** | Spesifikasyondan tipli parser/builder kodu üret |
| `Demo8-MultiModel/` | **Demo #8 — Multi-LLM Agent** | Çoklu-model AI Chain (Copilot CLI + Claude + Codex/Opus) |
| `Demo10-CICD/` | **Demo #10 — PR, CR ve CI/CD** | Workflow üretme/düzeltme, PR özeti, code review, pipeline log analizi |
| `.github/copilot-instructions.md` | **M13 — Repository Instructions** | Ekip standartlarını Copilot'a otomatik dayat |

> Her demo klasöründe ilgili tohum dosyaları ve (varsa) eğitmen cevap anahtarı bulunur.

---

## Demo #1 — Code Helper

**Dosya:** `Demo1-CodeHelper/OrderPriceCalculator.cs`
**Akış:**
1. Dosyayı aç, `@workspace /explain` → *"Bu metot ne yapıyor? Kıdemsiz bir geliştiriciye anlat."*
2. `/fix` → gizli null-reference ve edge-case'leri buldur.
3. *"Bu metodu SRP'ye (Single Responsibility Principle) göre refactor et, davranışı koru."*
4. `/doc` → XML doc + Türkçe özet üret.

**Vurgu:** Refactor sonrası davranış değişmedi mi? → test yazdırıp doğrula.

## Demo #2 — Security Review

**Dosya:** `Demo2-SecurityReview/InsecureUserService.cs`
**Prompt:** *"Kıdemli bir güvenlik uzmanı gibi bu dosyayı OWASP Top 10 açısından incele.
Her bulgu için: risk açıklaması, severity ve güvenli alternatif kod ver."*

**Beklenen bulgular:** SQL injection, MD5 hash, hardcoded secret/connection string,
backdoor parola, düz metin loglama, SSRF, path traversal, kapalı TLS doğrulaması.
(Tam liste dosyanın başındaki eğitmen yorumundadır.)

**Vurgu:** Copilot destekleyici bir taramadır; **SAST** araçlarının ve insan review'ın yerini almaz.

## Demo #7 — Doküman ve Protokol Analizi (ISO 8583)

**Dosyalar:** `Demo7-ISO8583/spec/ISO8583-Message-Spec.md` (spesifikasyon),
`Demo7-ISO8583/src/Iso8583Parser.cs` (tamamlanacak iskelet).
**Akış:**
1. Spec dosyasını Copilot bağlamına ekle (`#file`).
2. *"Bu spesifikasyona göre parser'daki TODO'ları doldur; bitmap ve alan tiplerini spec'e
   sadık uygula. Round-trip (Parse → Build) aynı ham mesajı üretmeli."*
3. Spec'teki örnek ham mesajı parse ettir ve alanları birlikte doğrula.
4. xUnit testleri ürettir.

**Vurgu:** İyi tanımlı protokollerde ciddi zaman kazandırır; ama spec'e sadakat **doğrulanmalıdır**.

## Demo #3 — Proje Analizi (MindMap)

**Klasör:** `Demo3-MindMap/src/` (katmanlı örnek uygulama), `EXPECTED-MindMap.md` (cevap anahtarı).
**Prompt:** *"@workspace Demo3-MindMap/src klasörünün katmanlı mimarisini çıkar, Mermaid diyagramı üret ve teknik borç alanlarını listele."*
**Vurgu:** Onboarding ve mimari gözden geçirmede dakikalar içinde harita. Tespitleri ekip teyit eder.

## Demo #4 — Test & UI Test Üretimi

**Klasör:** `Demo4-TestUI/src/DiscountService.cs` (testsiz servis), `ui/DeviceCard.tsx` (React bileşeni).
**Akış:** `/tests` ile xUnit testleri ürettir (happy + edge + exception); "eksik coverage'ı bul"; React bileşeni için RTL/Playwright UI testi ürettir.
**Vurgu:** Üretilen testleri **çalıştır**; yeşil/kırmızı kanıttır.

## Demo #5 — Vibe Coding

**Klasör:** `Demo5-VibeCoding/README-LiveScript.md` (adım adım eğitmen script'i).
**Akış:** Boş alanda agent modu ile "Contoso cihaz envanteri" mini uygulaması: fikir → plan → kod → hata düzeltme → test → iyileştirme → sonuç.
**Vurgu:** Prototip hızı; her adımda insan yönlendirir.

## Demo #6 — Görselden Kod

**Klasör:** `Demo6-ImageToCode/dashboard-mockup.png` (mockup), `README.md` (notlar), `make_mockup.py` (üretici).
**Akış:** Mockup görselini Chat'e ekle → *"Bu tasarımı responsive React + Tailwind bileşenine çevir."* → mobil iyileştirme.
**Vurgu:** Piksel-mükemmel değil; hızlı başlangıç, tasarım sistemine uyarlanır.

## Demo #8 — Multi-LLM / Çoklu-Model Agent (AI Chain)

**Klasör:** `Demo8-MultiModel/README.md` (7 demo), `scripts/run-multimodel-chain.md` (kopyala-yapıştır komutlar), `INSTRUCTOR-NOTES.md`.
**Akış:** plan (Opus) → çoklu öneri (Opus + GPT Codex, `/tasks`) → kod → `/review with opus, gpt 5.4 and sonnet 4.6` → test → commit; ayrıca Copilot CLI + Claude + iki MCP, session forking, utility model ayrımı.
**Vurgu:** Doğru işe doğru model + modeller arası çapraz kontrol. (Kaynak: OneNote "GitHub Copilot" notları.)

## Demo #10 — PR, CR ve CI/CD Süreçlerinde Copilot

**Klasör:** `Demo10-CICD/` — `src/` (.NET app + xUnit testleri), `starter/.github/workflows/ci.yml` (kasıtlı bozuk), `ANSWER-KEY/` (doğru ci.yml + cd.yml + PR template), `logs/failing-pipeline.log`, `prompts.md`, `README.md`.
**Akış:** CI workflow üret → bozuk workflow'u düzelt → PR açıklaması + `/review` → başarısız pipeline logunu analiz et.
**Vurgu:** Copilot teslim hattının her adımında yardımcı; workflow doğrulanmalı, secret YAML'a gömülmez, merge kararı insanda. (M14 modülünün canlı karşılığı.)

---

Prompt şablonları için bkz: `PROMPTS-CheatSheet.md`.

## Demo reset / cleanup (baştan başlatma)

Canlı demoyu yeniden temiz durumdan başlatmak için:

```powershell
.\scripts\Reset-DemoWorkspace.ps1 -Preview
.\scripts\Reset-DemoWorkspace.ps1 -Force
```

Alternatif kısa yol:

```bat
.\scripts\Reset-DemoWorkspace.bat -Force
```

Bu script `Demo-Repo` altındaki:
- tracked dosyalardaki değişiklikleri `HEAD`'e döndürür
- untracked/ignored artifact dosyalarını temizler (`git clean -fdx`)
