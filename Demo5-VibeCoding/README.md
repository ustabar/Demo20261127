# 🏭 Contoso Cihaz Envanteri (Device Inventory)

Basit bir **Node.js + Express** cihaz yönetim uygulaması. Cihazları listeleme, ekleme ve aktif/pasif filtreleme özelliklerine sahiptir.

## 🚀 Kurulum

### Adım 1: Bağımlılıkları Yükle
```bash
npm install
```

### Adım 2: Uygulamayı Çalıştır
```bash
# Development mode (auto-reload)
npm run dev

# Production mode
npm start
```

Uygulama **http://localhost:3000** adresinde çalışmaya başlayacak.

## 📁 Proje Yapısı

```
src/
├── server.js              # Express uygulaması
├── routes/
│   └── devices.js         # API endpoint'leri
├── controllers/
│   └── deviceController.js # İş mantığı
├── models/
│   └── Device.js          # Validasyon
├── middleware/
│   └── errorHandler.js    # Hata yönetimi
└── data/
    └── store.js           # In-memory veri deposu

public/
└── index.html             # Web arayüzü
```

## 🔌 API Endpoint'leri

### 📋 Cihazları Listele
```bash
GET /api/devices
GET /api/devices?isActive=true   # Sadece aktif cihazlar
GET /api/devices?isActive=false  # Sadece pasif cihazlar
```

**Response:**
```json
{
  "success": true,
  "count": 3,
  "data": [
    {
      "id": 1,
      "name": "Contoso 4K TV",
      "category": "tv",
      "isActive": true,
      "createdAt": "2026-08-14T10:00:00Z"
    }
  ]
}
```

### 🔍 Tek Cihaz Getir
```bash
GET /api/devices/:id
```

### ➕ Yeni Cihaz Ekle
```bash
POST /api/devices
Content-Type: application/json

{
  "name": "Contoso Klima",
  "category": "whitegoods",  # "tv" | "whitegoods" | "accessory"
  "isActive": true           # Optional, default: true
}
```

**Response (201 Created):**
```json
{
  "success": true,
  "message": "Device created successfully",
  "data": {
    "id": 4,
    "name": "Contoso Klima",
    "category": "whitegoods",
    "isActive": true,
    "createdAt": "2026-08-14T11:30:00Z"
  }
}
```

### ✏️ Cihaz Güncelle
```bash
PUT /api/devices/:id
Content-Type: application/json

{
  "name": "Contoso Klima Pro",
  "isActive": false
}
```

### 🗑️ Cihaz Sil
```bash
DELETE /api/devices/:id
```

### 📊 İstatistik
```bash
GET /api/devices/stats/summary
```

**Response:**
```json
{
  "success": true,
  "data": {
    "total": 3,
    "active": 2,
    "inactive": 1,
    "byCategory": {
      "tv": 1,
      "whitegoods": 2
    }
  }
}
```

## 💾 Veri Modeli

```javascript
{
  id: number,          // Unique identifier
  name: string,        // Min 3 karakter
  category: string,    // "tv" | "whitegoods" | "accessory"
  isActive: boolean,   // Aktif/Pasif durumu
  createdAt: string    // ISO 8601 timestamp
}
```

## ✅ Validasyonlar

- **name**: Required, string, min 3 karakter, unique
- **category**: Required, enum ["tv", "whitegoods", "accessory"]
- **isActive**: Optional boolean (default: true)

## 🎨 Web Arayüzü

Browser'da **http://localhost:3000** açarak:
- ✅ Tüm cihazları görüntüleyin
- ✅ Aktif/Pasif cihazları filtreleyin
- ✅ Yeni cihaz ekleyin
- ✅ İstatistikleri görmek

## 🔧 Özellikler

✅ RESTful API  
✅ In-memory veri deposu (Singleton)  
✅ Input validasyonu  
✅ Error handling  
✅ Responsive web UI  
✅ Türkçe/İngilizce arayüz  
✅ Real-time istatistikler  

## ⚠️ Teknik Notlar

- Veri **bellekte tutulur** (uygulama restart'ında kaybolur)
- **Veritabanı** entegrasyonu yapılmamıştır
- **Authentication** uygulanmamıştır
- **CORS** konfigürasyonu yapılmamıştır (local only)

## 🎓 Öğrenme Amaçlı

Bu proje, temel Node.js + Express mimarisi, REST API design ve front-end integration öğrenmek için oluşturulmuştur.

---

**Contoso Demo Project** 🏭
