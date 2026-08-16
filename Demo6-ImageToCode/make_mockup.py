from pathlib import Path
from PIL import Image, ImageDraw, ImageFont
import os

W, H = 1280, 860
NAVY = (30, 39, 97)
NAVY_D = (18, 23, 52)
GOLD = (245, 184, 65)
INK = (32, 38, 74)
MUTED = (120, 128, 150)
LIGHT = (238, 242, 251)
CARD = (255, 255, 255)
GREEN = (46, 160, 100)
RED = (200, 70, 70)
LINE = (223, 230, 242)

img = Image.new("RGB", (W, H), LIGHT)
d = ImageDraw.Draw(img)

def font(sz, bold=False):
    names = (["seguisb.ttf", "segoeuib.ttf"] if bold else ["segoeui.ttf"])
    for n in names:
        for base in [r"C:\Windows\Fonts"]:
            p = os.path.join(base, n)
            if os.path.exists(p):
                try:
                    return ImageFont.truetype(p, sz)
                except Exception:
                    pass
    return ImageFont.load_default()

def rr(box, r, fill, outline=None, width=1):
    d.rounded_rectangle(box, radius=r, fill=fill, outline=outline, width=width)

# ---- Sidebar ----
d.rectangle([0, 0, 230, H], fill=NAVY_D)
d.ellipse([40, 34, 74, 68], fill=GOLD)
d.text((86, 40), "Contoso", font=font(24, True), fill=(255, 255, 255))
nav = ["Genel Bakis", "Cihazlar", "Raporlar", "Ayarlar"]
for i, item in enumerate(nav):
    y = 130 + i * 54
    if i == 1:
        rr([16, y - 10, 214, y + 34], 8, NAVY)
        d.rectangle([16, y - 10, 20, y + 34], fill=GOLD)
    col = (255, 255, 255) if i == 1 else (150, 158, 190)
    d.text((40, y), item, font=font(16, i == 1), fill=col)

# ---- Topbar ----
d.rectangle([230, 0, W, 84], fill=CARD)
d.line([230, 84, W, 84], fill=LINE, width=1)
d.text((262, 28), "Cihaz Yonetimi", font=font(24, True), fill=NAVY)
rr([W - 210, 24, W - 40, 60], 18, LIGHT)
d.text((W - 190, 32), "Yeni Cihaz  +", font=font(15, True), fill=NAVY)
d.ellipse([W - 260, 26, W - 226, 60], fill=NAVY)

# ---- KPI cards ----
kpis = [("Toplam Cihaz", "1.248", GOLD), ("Aktif", "1.032", GREEN),
        ("Pasif", "216", RED), ("Bu Ay Eklenen", "84", NAVY)]
cx = 262
cw = 232
for i, (label, val, ac) in enumerate(kpis):
    x = cx + i * (cw + 16)
    rr([x, 108, x + cw, 220], 12, CARD, outline=LINE, width=1)
    d.rectangle([x, 108, x + 6, 220], fill=ac)
    d.text((x + 24, 128), label, font=font(14), fill=MUTED)
    d.text((x + 24, 152), val, font=font(40, True), fill=INK)

# ---- Chart card ----
rr([262, 244, 262 + 470, 244 + 300], 12, CARD, outline=LINE, width=1)
d.text((286, 262), "Kategori Dagilimi", font=font(17, True), fill=NAVY)
bars = [("TV", 420, GOLD), ("Beyaz Esya", 610, NAVY), ("Aksesuar", 218, GREEN)]
bx = 300
base_y = 500
maxv = 640
for i, (lbl, v, c) in enumerate(bars):
    x = bx + i * 140
    bh = int(v / maxv * 190)
    rr([x, base_y - bh, x + 80, base_y], 6, c)
    d.text((x, base_y + 12), lbl, font=font(13), fill=MUTED)
    d.text((x, base_y - bh - 22), str(v), font=font(14, True), fill=INK)

# ---- Device table card ----
tx = 262 + 470 + 16
tw = W - tx - 40
rr([tx, 244, tx + tw, 244 + 300], 12, CARD, outline=LINE, width=1)
d.text((tx + 24, 262), "Son Cihazlar", font=font(17, True), fill=NAVY)
# header
d.text((tx + 24, 300), "Cihaz", font=font(13, True), fill=MUTED)
d.text((tx + 250, 300), "Kategori", font=font(13, True), fill=MUTED)
d.text((tx + 400, 300), "Durum", font=font(13, True), fill=MUTED)
d.line([tx + 24, 324, tx + tw - 24, 324], fill=LINE, width=1)
rows = [("Contoso 4K TV 55\"", "TV", True),
        ("Contoso Buzdolabi", "Beyaz Esya", True),
        ("Contoso Klima A+", "Beyaz Esya", False),
        ("Contoso Kulaklik", "Aksesuar", True),
        ("Contoso Camasir M.", "Beyaz Esya", True)]
for i, (name, cat, active) in enumerate(rows):
    y = 340 + i * 38
    d.text((tx + 24, y), name, font=font(14), fill=INK)
    d.text((tx + 250, y), cat, font=font(14), fill=MUTED)
    chip = GREEN if active else RED
    label = "Aktif" if active else "Pasif"
    rr([tx + 400, y - 2, tx + 470, y + 22], 11, (chip[0], chip[1], chip[2]))
    d.text((tx + 414, y + 1), label, font=font(12, True), fill=(255, 255, 255))

# ---- Footer note ----
d.text((262, 566), "Mockup — Demo #6 (Gorselden Kod). Bu tasarimi Copilot'a verip React + Tailwind bilesenine cevirtin.",
       font=font(13), fill=MUTED)

out = str(Path(__file__).with_name("dashboard-mockup.png"))
img.save(out)
print("WROTE", out)
