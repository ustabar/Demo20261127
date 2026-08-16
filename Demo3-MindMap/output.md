# ContosoDemo.Inventory Layered Architecture
## Presentation Layer (Sunuş Katmanı)
- DeviceController
  - Bağımlılıklar
    - DeviceService (new DeviceService())
    - ReportService (new ReportService())
  - İşlemler
    - GetById(int id)
    - List()
    - Add(Device device)
    - DailyReport()
  - Sorunlar
    - Dependency Injection yok
    - Sert coupling (hard coupling)

## Business Logic Layer (İş Mantığı Katmanı)
- DeviceService
  - Bağımlılıklar
    - DeviceRepository (new DeviceRepository())
    - Device Model
  - İşlemler
    - GetDevice(int id)
    - GetAllDevices()
    - AddDevice(Device device)
    - GetActiveDevices()
  - Sorunlar
    - Validasyon yok
    - Repository direkt instantiation

- ReportService
  - Bağımlılıklar
    - DeviceRepository (new DeviceRepository()) ⚠️ KATMAN ATLAMA
    - Device Model
  - İşlemler
    - BuildDailyReport()
  - Sorunlar
    - Service → Repository direkt erişim (mimari ihlal)
    - Ayrı repository instance oluşturuluyor

## Data Access Layer (Veri Erişim Katmanı)
- DeviceRepository
  - Bağımlılıklar
    - Device Model
    - In-memory List<Device> (_store)
  - İşlemler
    - FindById(int id)
    - GetAll()
    - Insert(Device device)
  - Sorunlar
    - Static List (Thread-safety yok)
    - Hiç duplicate kontrol yok
    - Her Service ayrı instance oluşturuyor
    - Kalıcı depolama yok

## Model Layer (Model Katmanı)
- Device
  - Özellikler
    - int Id
    - string Name
    - string Category ("tv" | "whitegoods" | "accessory")
    - bool IsActive

## Bağımlılık Akışı (Dependency Flow)
- DeviceController
  ↓ (new)
  - DeviceService ←→ ReportService
  ↓ (new)          ↓ (new / LAYER SKIP)
  - DeviceRepository ← DeviceRepository
  ↓
  - Device Model