# Demo #6 — Görselden Kod (M20): Eğitmen Notları

**Dosya:** `dashboard-mockup.png` — Contoso cihaz yönetimi dashboard mockup'ı.
**Amaç:** Bir tasarım görselini Copilot'a verip çalışan UI koduna çevirmeyi göstermek.

---

## Akış

1. Copilot Chat'i aç, `dashboard-mockup.png` görselini sohbete **ekle** (sürükle-bırak veya ekle butonu).
2. Prompt:
   ```
   Ekteki mockup'ı temel al. Bu dashboard'u responsive bir React + Tailwind CSS
   bileşenine çevir. Sol menü, üstteki 4 KPI kartı, kategori bar grafiği ve
   son cihazlar tablosunu (Aktif/Pasif durum çipleriyle) içersin. Erişilebilirlik
   temel kurallarına uy. Tek dosyalık bir bileşen üret.
   Yaratacağın dosyalar için sana verdiğim folder'ı kullan
   ```
3. Üretilen bileşeni gözden geçir; renk/boşluk uyumunu mockup ile karşılaştır.
4. İyileştirme:
   ```
   KPI kartlarını mobilde alt alta getir, tabloyu küçük ekranda yatay kaydırılabilir yap.
   ```

## Eğitmen Vurguları

- Sonuç **piksel-mükemmel değildir** — amaç %80'i saniyeler içinde üretip hızlı başlangıç sağlamaktır.
- Üretilen kod ekibin **tasarım sistemine / component kütüphanesine** uyarlanır.
- Görsel + metin birlikte verildiğinde Copilot bağlamı çok daha iyi kullanır.

## Yeniden Üretim

Mockup'ı değiştirmek isterseniz `make_mockup.py` (Pillow) ile yeniden üretebilirsiniz:
```
python make_mockup.py
```
