# Demo #8 — Multi-LLM / Çoklu-Model Agent Kullanımı (M12 AI Chain)

> **Kaynak:** Bu demo seti, OneNote "BakilacakLinkler → GitHub Copilot" sayfasındaki notlardan
> derlenmiş **gerçek** multi-LLM/çoklu-model agent iş akışlarına dayanır.
> **Amaç:** Farklı LLM'leri (Copilot, Codex/GPT, Claude/Sonnet, Opus) aynı iş akışında —
> plan, kod, review, araştırma — **rol bazlı** kullanmayı canlı göstermek.
> **Eğitim modülü:** M12 (AI Chain) + M8 (Agent) + M9 (MCP) + M11 (Token Optimization).

---

## Ön Koşullar

- **GitHub Copilot CLI** kurulu ve oturum açık (`copilot` komutu çalışıyor).
- VS Code **Agents** penceresi (v1.120+ Stable).
- Çoklu-model erişimi (Copilot dahilinde Opus / GPT-5.x / Sonnet 4.6 seçilebiliyor olmalı) veya BYOK.
- Session forking için ayar: `github.copilot.chat.cli.forkSessions.enabled = true`.
- (Demo A için) MCP sunucuları: **GitHub MCP** ve **Microsoft Learn MCP**.

> ⚠️ Model adları (Opus, GPT-5.3 Codex, GPT-5.4, Sonnet 4.6) örnektir; ortamınızda mevcut
> model isimleriyle değiştirin. Komutlar canlı LLM içerdiğinden **önceden test edin** ve yedek
> ekran görüntüsü/kayıt bulundurun.

---

## Demo A — İki Ajan + İki MCP Kaynağı (Copilot CLI + Claude)

**Fikir:** Kodlama işini bir ajana (Copilot CLI + GitHub MCP), dokümantasyon araştırmasını
başka bir ajana (Claude + Microsoft Learn MCP) paralel yaptır.

**Session 1 — Copilot CLI agent + GitHub MCP Server**
```
List the open issues in my repository
Start working on issue 26. First update the description to provide more detail,
then start the implementation.
```

**Session 2 — Claude agent + Microsoft Learn MCP Server**
```
Search the Microsoft docs to lookup info on the conversion from .NET 9 to .NET 10.
Outline the high level steps in a concise numbered list. I am a seasoned .NET developer.
Don't make the response too bloated.
```

**Vurgu:** Farklı modeller + farklı bilgi kaynakları aynı anda; her ajan güçlü olduğu işte.

---

## Demo B — Session Forking + Model Değiştirme

**Fikir:** Aynı bağlamı koru, bir noktadan sonra **modeli değiştirip** görüşleri karşılaştır.

```
List 3 ways to refactor this method to improve its maintainability and readability
Name the pros and cons of option 3
```
→ Konuşmayı **fork** et, sonraki sorular için LLM modelini değiştir (örn. Sonnet → Opus).

Ayar:
```
github.copilot.chat.cli.forkSessions.enabled = true
```

**Vurgu:** "İkinci bir görüş" — aynı bağlam üzerinde farklı modelin yaklaşımı.

---

## Demo C — Nested Subagents (Görev Dağıtımı)

**Fikir:** Bir ana ajan, alt görevleri farklı **subagent**'lara dağıtsın; sonuçlar sonda toplansın.

```
Break this task into independent subtasks and assign each to a separate subagent:
1) Add input validation to the RegisterUser method
2) Write xUnit tests for RegisterUser
3) Update the README with the new validation rules
Run them and collect the results at the end.
```
Debug/izleme:
```
Show Agent Debug Logs
```

**Vurgu:** Paralel görev yürütme + sonuçların birleştirilmesi (orkestrasyon).

---

## Demo D — Çoklu Modelle Paralel Öneri (Copilot CLI)

**Fikir:** İki farklı model, aynı problem için iki bağımsız çözüm önerisi üretsin; yan yana kıyasla.

```
Use Opus and GPT-5.3 Codex, and get two different pseudocode proposals on how we might
add list item editing to this application. Use the same structure in each one of these
proposals, have each subagent create one proposal in the "model-approaches" folder.
```
Paralel aksiyonları izle:
```
/tasks
```
Ardından değerlendir:
```
Explain these two proposals, then find the parts where the answer is missing or could be
better, improve them, and give me the result.
```

**Vurgu:** Model çeşitliliği = fikir çeşitliliği; plan/öneri aşamasında çoklu model.

---

## Demo E — Çoklu Modelle Code Review

**Fikir:** Aynı değişikliği üç farklı modele review ettir, görüşleri harmanla.

```
/review with opus, gpt 5.4 and sonnet 4.6
```

**Vurgu:** Review aşamasında tek modele bağımlı kalmama; kör noktaları azaltma. (M14 PR/CR ile birleşir.)

---

## Demo F — VS Code Agents: Tüm Ajanlara Erişim

**Fikir:** Copilot, Codex ve Claude ajanlarını tek arayüzden (Agents penceresi), çoklu depo +
eşzamanlı seans ile yönetmek.

- Editör üstünde **"Open in Agents"** → yan yana oturumlar, Pin, çoklu-repo.
- Referans video: *Access Every Agent in VS Code - Copilot, Codex, & Claude*
  (youtube.com/watch?v=OWYQ8Mn7KqE)

**Vurgu:** Tek pencerede çok-ajan/çok-model orkestrasyonu.

---

## Demo G — Maliyet-Bilinçli Çoklu Model (Utility Model Ayrımı)

**Fikir:** Ağır işe güçlü model, arka plan işine ekonomik model.

Ayarlar:
```
chat.utilityModel        # başlık, özet, ayar arama gibi orta işler
chat.utilitySmallModel   # commit mesajı, rename, branch adı, intent — hızlı/ucuz
```

**Vurgu:** Doğru işe doğru model = kalite + maliyet dengesi (M11 Token Optimization).

---

## Tam Zincir Önerisi (AI Chain — uçtan uca)

Bir özelliği baştan sona farklı rollerde farklı modellerle geçir:

| Aşama | Rol | Örnek Model | Komut/Prompt |
|------|-----|-------------|--------------|
| 1. Plan | Reasoning | Opus | "Bu özellik için adım adım plan çıkar" |
| 2. Kod | Hızlı/Codex | GPT-5.3 Codex | "Bu plana göre iskeleti üret" |
| 3. Review | Farklı model | Sonnet 4.6 | `/review with opus, gpt 5.4 and sonnet 4.6` |
| 4. Test | Hızlı | GPT-5.4 mini | "Eksik testleri üret ve çalıştır" |
| 5. Commit | Küçük utility | utilitySmallModel | "Conventional Commits formatında commit mesajı" |

`run-multimodel-chain.md` (scripts/) bu zinciri kopyala-yapıştır komut bloğu olarak içerir.
