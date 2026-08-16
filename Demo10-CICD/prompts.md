# Demo #10 — CI/CD Prompt Şablonları

Kopyala-yapıştır komutlar. VS Code + GitHub Copilot Chat / CLI içinde kullanılır.

## 1) Sıfırdan CI workflow üret
```
@workspace Bu .NET 8 çözümü için bir GitHub Actions CI workflow'u üret.
- push (main) ve pull_request tetikleyicileri olsun
- restore, build (Release), test adımları olsun
- test sonuçlarını (trx) artifact olarak yükle
Dosyayı .github/workflows/ci.yml olarak öner.
```

## 2) Bozuk workflow'u düzelt (starter/.github/workflows/ci.yml)
```
@workspace starter/.github/workflows/ci.yml çalışmıyor. Tüm hataları bul, neden
hatalı olduklarını açıkla ve düzeltilmiş tam dosyayı ver.
```

## 3) CD (deploy) workflow'u ekle
```
Azure Web App'e deploy eden bir CD workflow'u üret. Sürüm etiketi (v*) push'unda
tetiklensin, OIDC ile azure/login kullansın, secret'ları GitHub Secrets'tan alsın.
```

## 4) PR açıklaması üret
```
Bu branch'teki değişiklikler için .github/pull_request_template.md şablonuna uygun
bir PR açıklaması yaz. Özet, değişiklikler, test ve kontrol listesini doldur.
```

## 5) Code review (PR üzerinde)
```
/review
```
veya çoklu model:
```
/review with opus, gpt 5.4 and sonnet 4.6
```

## 6) Başarısız pipeline logunu analiz et (logs/failing-pipeline.log)
```
Bu CI log çıktısını analiz et. Pipeline neden başarısız oldu? Kök nedeni bul,
kodun mu testin mi hatalı olduğunu belirt ve somut düzeltme adımları öner.
```

## 7) Dockerfile + container CI (opsiyonel)
```
Bu API için çok aşamalı (multi-stage) bir Dockerfile üret ve CI'a imajı build edip
GitHub Container Registry'ye push eden bir adım ekle.
```

## 8) Pipeline'ı iyileştir
```
Bu CI workflow'una şunları ekle: bağımlılık önbellekleme (cache), matrix ile
.NET 8 ve 9 üzerinde test, ve main'e merge'de code coverage raporu.
```
