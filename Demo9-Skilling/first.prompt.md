copilot-instructions.md kurallarına göre şu sorunu analiz et: 

.NET Framework katmanında açılan transaction, ardışık SP çağrılarıyla devam ediyor; SP'lerin bazılarında ROLLBACK var; sonuçta muhasebe kayıtları oluşmuyor ancak akış hata vermeden tamamlanıyor gibi görünüyor. 

Sırayla: 

(1) .NET transaction sınırını ve Commit/Complete çağrılarını tüm code path'lerde çıkar. 

(2) SP çağrı zincirini ve her SP'nin transaction/hata yönetimi matrisini oluştur. 

(3) Savepoint'siz ROLLBACK ve THROW'suz CATCH kalıplarını öncelikli olarak işaretle. 

(4) Muhasebe INSERT'inin transaction kapsamındaki konumunu belirle. 

Bulguları ROOT_CAUSE.md içine kanıt ve güven seviyesiyle yaz. 

Hiçbir dosyayı değiştirme.