# Demo #8 — Eğitmen Notları (Multi-LLM Agent)

## Amaç ve Mesaj

Bu demo, "tek modelden her şeyi bekleme" anlayışından **rol bazlı çoklu-model** anlayışına
geçişi gösterir. Ana mesaj: **doğru işe doğru model** ve **modeller arası çapraz kontrol**.

## Zamanlama (~12-15 dk)

1. (2 dk) Kısa çerçeve: neden çoklu model? (fikir çeşitliliği, kör nokta azaltma, maliyet)
2. (4 dk) **Demo D** — Opus + GPT Codex paralel öneri + `/tasks`
3. (3 dk) **Demo E** — `/review with opus, gpt 5.4 and sonnet 4.6`
4. (3 dk) **Demo B** — session fork + model değiştirme
5. (2 dk) Kapanış: utility model ayrımı (maliyet) + AI Chain tablosu

> Zaman azsa: Demo D + E yeterli. Demo A (iki MCP) kurulum gerektirir; yalnızca MCP hazırsa yapın.

## Canlı Demo Uyarıları

- **Model adları değişkendir.** Ortamdaki gerçek modelleri (Opus/GPT-5.x/Sonnet) önceden seçili tut.
- **Önceden test et** ve yedek ekran görüntüsü/kayıt bulundur — canlı LLM değişkendir.
- Farklı modeller **farklı çıktı** verecek; bunu avantaj olarak sun: "işte tam da bu yüzden çapraz kontrol."
- Maliyet vurgusu: usage-based billing'de her model bir maliyet — utility model ayrımını göster.

## Sık Sorular

- *"Hangi modeli ne zaman?"* → Reasoning = plan/mimari/derin analiz; hızlı/Codex = kod üretimi;
  farklı 2. model = review/çapraz kontrol; küçük utility = commit/rename/intent.
- *"Bu pahalı değil mi?"* → Rol ayrımı doğru yapılırsa (küçük işe küçük model) genelde daha ucuz + daha kaliteli.
- *"Copilot dışı modeller (Claude/Codex) nasıl?"* → VS Code Agents + BYOK / MCP ile aynı arayüzde.

## Contoso Eğitimindeki Yeri

- **M12 (AI Chain)** ana modül; **M8 (Agent)**, **M9 (MCP)**, **M11 (Token Optimization)**,
  **M14 (PR/CR — /review)** ile doğrudan bağlantılı.
- Slaytlardaki AI Chain akış diyagramına (plan→kod→review→test→commit) bu gerçek komutlar eklenebilir.

## Kaynak

Notların kaynağı: OneNote "BakilacakLinkler → GitHub Copilot" sayfası. Referans makaleler:
- Why Multiple Models Matter: GitHub Copilot CLI's Subagent Architecture
  (fbakkensen.github.io/ai/copilot/productivity/2026/04/10/github-copilot-cli-multi-model-subagents.html)
- Multiple models in GitHub Copilot (LinkedIn, Flemming Bakkensen)
- Access Every Agent in VS Code - Copilot, Codex, & Claude (youtube.com/watch?v=OWYQ8Mn7KqE)
