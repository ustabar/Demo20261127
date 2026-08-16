# Amaç
.NET Framework katmanından başlayıp SQL Server SP zinciriyle devam eden transactional
bir iş akışında, beklenmedik ROLLBACK nedeniyle muhasebe kayıtlarının oluşmamasının
kök nedenini bulmak. Kod DEĞİŞTİRME; kanıta dayalı analiz raporu üret.
 
# Ortam
- PROD: yalnızca okuma (şema, SP metni, Extended Events çıktısı).
- REPRO: restore edilmiş kopya; burada test çalıştırması yapılabilir.
- Tüm DB erişimi `sqlcmd` ile: -b -t 60 -W -s"|" -h-1, sorgular ./analysis/queries/*.sql,
  çıktı ./analysis/output/. Credential yazma/loglama.
 
# Analiz Adımları (sırayla)
1. .NET katmanında transaction sınırını tespit et: TransactionScope mu, SqlTransaction mı,
   IsolationLevel, Timeout, Complete()/Commit() çağrılarının tüm code path'lerde
   yapılıp yapılmadığı, using/dispose davranışı, retry mantığı var mı.
2. Giriş SP'sinden başlayarak SP çağrı grafiğini çıkar (sys.sql_expression_dependencies
   + SP metni taraması). Zinciri ağaç olarak raporla.
3. Her SP için şu matrisi doldur:
   SP adı | SET XACT_ABORT | BEGIN TRAN var mı | COMMIT koşulu | ROLLBACK var mı |
   ROLLBACK savepoint'li mi | TRY/CATCH var mı | CATCH'te THROW/RAISERROR var mı |
   @@TRANCOUNT kontrolü yapıyor mu | XACT_STATE() kontrolü yapıyor mu
4. Şu anti-pattern'leri YÜKSEK önem ile işaretle:
   - CATCH içinde ROLLBACK var ama THROW/RAISERROR yok (hata yutuluyor)
   - Savepoint'siz ROLLBACK (dıştaki transaction'ı komple geri alır)
   - @@TRANCOUNT kontrolü olmadan COMMIT/ROLLBACK
   - XACT_ABORT ON iken kısmi rollback beklentisi
   - Hata logunun aynı transaction içindeki normal tabloya yazılması (log da rollback olur)
5. Muhasebe kaydını yazan INSERT'i bul. Transaction sınırına göre konumunu belirle:
   rollback kapsamında mı, ayrı connection'da mı, hangi SP'den sonra çalışıyor.
6. REPRO ortamında zinciri çalıştır ve her adımda @@TRANCOUNT ile XACT_STATE() değerini
   kaydet. Rollback'in tetiklendiği ilk noktayı tespit et.
7. Extended Events oturumu öner (error_reported severity>=11, sql_transaction,
   rpc_completed, lock_deadlock) ve çıktısını yorumla.
 
# Çıktı
./analysis/ROOT_CAUSE.md
- Bölüm 1: Transaction sınır haritası (.NET → SP zinciri, @@TRANCOUNT seviyeleri)
- Bölüm 2: SP matrisi (yukarıdaki tablo)
- Bölüm 3: Bulgular — her biri: Hipotez | Önem | Dosya/SP:Satır | Kanıt | Neden bu sonucu doğuruyor
- Bölüm 4: Kök neden değerlendirmesi — güven seviyesi (Yüksek/Orta/Düşük) ile
- Bölüm 5: Doğrulama planı — kök nedeni ispatlayacak somut test adımları
 
# Kurallar
- Kanıtsız iddia yazma. Kanıt = SP metni alıntısı, kod satırı veya sqlcmd/XE çıktısı.
- Emin değilsen "doğrulanamadı — şu veri gerekli" yaz. Tahmin yürütme.
- Hiçbir kaynak dosyayı veya SP'yi değiştirme.