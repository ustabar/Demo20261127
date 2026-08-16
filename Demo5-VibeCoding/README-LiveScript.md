# Demo #5 — Vibe Coding (M19): Eğitmen Canlı Script'i

**Amaç:** Doğal dille, sıfırdan küçük çalışan bir uygulama geliştirmeyi canlı göstermek.
Bu klasör bilinçli olarak **boştur** — demo, boş bir alanda Copilot agent modu ile yürütülür.

**Süre:** ~15 dk · **En yüksek enerji anı** — katılımcıları prompt'lara dahil edin.

---

## Proje Brief'i

> **"Cihaz Envanteri"** — küçük bir REST API + basit web arayüzü.
> Cihazları listeler, yeni cihaz ekler, aktif/pasif filtreler. Kalıcılık bellek-içi olabilir.

Teknoloji önerisi (birini seçin): **Node.js + Express** (hızlı) veya **.NET Minimal API**.

---

## Adım Adım Akış

### 1. Fikir → Plan
Chat/agent'a:
```
Küçük bir "cihaz envanteri" uygulaması yapacağız: cihazları listeleme,
ekleme ve aktif/pasif filtreleme. Node.js + Express kullan, veriyi bellekte tut.
Önce kod yazmadan adım adım bir uygulama planı çıkar.
```
→ Copilot'un planını katılımcılarla birlikte gözden geçirin.

### 2. Kod Üretimi
```
Bu plana göre projeyi oluştur sana referans verdiğim klasörde: package.json, Express sunucusu, GET /devices, POST /devices, GET /devices?active=true endpoint'leri ve basit bir index.html arayüzü.
```
→ Agent dosyaları oluştursun. Ne yaptığını sesli anlatın.

### 3. Çalıştırma & Hata Düzeltme
```
Uygulamayı çalıştır.
```
Bir hata çıkarsa (port, eksik bağımlılık vb.) hatayı **olduğu gibi** yapıştırın:
```
Şu hatayı aldım: <hata metni>. Nedenini açıkla ve düzelt.
```

### 4. Test Üretimi
```
POST /devices ve filtreleme mantığı için birkaç otomatik test ekle ve çalıştır.
```

### 5. İyileştirme
```
Giriş doğrulaması ekle (boş isim reddedilsin), uygun HTTP durum kodları döndür
ve hataları düzgün yönet.
```

### 6. Sonuç
Çalışan uygulamayı tarayıcıda gösterin. Bir cihaz ekleyin, filtreyi deneyin.

---

## Eğitmen Vurguları

- **Hız etkileyici** ama her adımda **insan yönlendiriyor** — Copilot tek başına karar vermiyor.
- Bu **prototip hızıdır**; üretilen kod prod'a doğrudan gitmez, review'dan geçer.
- Katılımcıları dahil edin: *"Şimdi ne ekleyelim?"* → canlı prompt.
- Bir şey ters giderse panik yok: hatayı yapıştır-düzelt döngüsü demonun en öğretici kısmıdır.

## Yedek Plan

Canlı ortam sorun çıkarırsa: önceden kaydedilmiş bir ekran videosu veya bu brief'i
tek prompt olarak verip sonucu göstermeye hazır olun.
