# Multi-Model AI Chain — Kopyala/Yapıştır Komut Blokları

> Eğitmen için hazır komut dizileri. Her blok Copilot CLI / VS Code Agents içinde sırayla çalıştırılır.
> Model adlarını ortamınızdaki gerçek modellerle değiştirin.

## 0) Ayarları aç (bir kez)
```
# VS Code settings.json
"github.copilot.chat.cli.forkSessions.enabled": true
"chat.utilityModel": "<orta-model>"
"chat.utilitySmallModel": "<kucuk-model>"
```

## 1) PLAN — reasoning model (örn. Opus)
```
Ben kıdemli bir .NET geliştiricisiyim. "Cihaz envanterine liste öğesi düzenleme" özelliği
ekleyeceğiz. Kod yazmadan önce adım adım, numaralı ve sade bir uygulama planı çıkar.
```

## 2) ÇOKLU ÖNERİ — iki model paralel (Opus + GPT Codex)
```
Use Opus and GPT-5.3 Codex, and get two different pseudocode proposals on how we might
add list item editing to this application. Use the same structure in each proposal, and
have each subagent write its proposal into the "model-approaches" folder.
```
```
/tasks
```

## 3) KOD — hızlı/Codex model
```
"model-approaches" klasöründeki iki öneriyi karşılaştır, en sağlamını seç ve bu plana göre
gerçek kodu üret. Mevcut public API'yi bozma, decimal kullan, Türkçe yorum yaz.
```

## 4) REVIEW — çoklu model
```
/review with opus, gpt 5.4 and sonnet 4.6
```

## 5) TEST — hızlı model
```
Bu değişiklik için xUnit testleri üret (happy + edge + exception) ve çalıştır.
Eksik coverage dallarını da işaretle.
```

## 6) COMMIT — küçük utility model
```
Bu diff için Conventional Commits formatında, Türkçe kısa bir commit mesajı yaz.
```

---

## Ek: İki Ajan + İki MCP (paralel araştırma + kodlama)

### Session 1 — Copilot CLI + GitHub MCP
```
List the open issues in my repository
Start working on issue 26. First update the description to provide more detail,
then start the implementation.
```

### Session 2 — Claude + Microsoft Learn MCP
```
Search the Microsoft docs to lookup info on the conversion from .NET 9 to .NET 10.
Outline the high level steps in a concise numbered list. I am a seasoned .NET developer.
Don't make the response too bloated.
```

---

## Ek: Session Forking (aynı bağlam, farklı model)
```
List 3 ways to refactor this method to improve its maintainability and readability
Name the pros and cons of option 3
# -> konuşmayı fork et, sonraki sorular için modeli değiştir (Sonnet -> Opus)
Now, using the other model, challenge option 3 and propose a stronger alternative.
```
