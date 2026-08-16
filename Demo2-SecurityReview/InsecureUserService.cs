using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

namespace ContosoDemo.Insecure
{
    // ============================================================================
    // DEMO #2 - SECURITY REVIEW (M16)
    // ----------------------------------------------------------------------------
    // Bu dosya KASITLI olarak birden fazla guvenlik acigi icerir. Egitmen Copilot'a:
    //   Prompt: "Kidemli bir guvenlik uzmani gibi bu dosyayi OWASP Top 10 acisindan
    //            incele. Her bulgu icin: (1) risk aciklamasi, (2) severity, (3)
    //            guvenli alternatif kod ornegi ver."
    // seklinde sorar ve bulgulari canli olarak duzelttirir.
    //
    // BEKLENEN BULGULAR (egitmen cevap anahtari):
    //  [A03 Injection]        -> AuthenticateUser: SQL injection (string concat)
    //  [A02 Crypto Failures]  -> HashPassword: MD5 kullanimi (zayif, tuzsuz)
    //  [A07 Auth Failures]    -> Sabit/gomulu admin sifresi
    //  [A05 Misconfig]        -> Hardcoded connection string + gomulu API anahtari
    //  [A09 Logging]          -> Sifre ve token'in duz metin loglanmasi
    //  [SSRF / A10]           -> FetchProfileImage: dogrulanmamis URL ile istek
    //  [Path Traversal]       -> ReadUserFile: kullanici girdisiyle dosya yolu
    //  [Insecure TLS]         -> Sertifika dogrulamasinin devre disi birakilmasi
    // ============================================================================

    public class InsecureUserService
    {
        // A05: Hardcoded connection string + credentials
        private const string ConnectionString =
            "Server=<DEMO_DB_HOST>;Database=ContosoUsers;User Id=<DEMO_USER>;Password=<DEMO_PASSWORD>;";

        // A05: Hardcoded secret / API key
        private const string ApiKey = "<DEMO_API_KEY>";
        private const string AdminBackdoorPassword = "<DEMO_ADMIN_PASSWORD>"; // A07

        // A03: SQL Injection - kullanici girdisi dogrudan sorguya gomulu
        public bool AuthenticateUser(string username, string password)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = '"
                               + username + "' AND PasswordHash = '" + HashPassword(password) + "'";
                var cmd = new SqlCommand(query, conn);

                // A09: Hassas veri duz metin loglaniyor
                Console.WriteLine("[LOGIN] user=" + username + " pass=" + password + " query=" + query);

                // A07: Backdoor / gomulu admin sifresi
                if (password == AdminBackdoorPassword) return true;

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // A02: MD5 - kriptografik olarak kirik, tuzsuz hash
        public string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // A10/SSRF: Dogrulanmamis kullanici URL'sine sunucu tarafli istek
        public byte[] FetchProfileImage(string imageUrl)
        {
            // Insecure TLS: sertifika dogrulamasi tamamen kapatilmis
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, err) => true;

            var request = (HttpWebRequest)WebRequest.Create(imageUrl);
            request.Headers.Add("X-Api-Key", ApiKey); // gizli anahtar disari sizabilir
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var ms = new MemoryStream())
            {
                response.GetResponseStream().CopyTo(ms);
                return ms.ToArray();
            }
        }

        // Path Traversal: kullanici girdisi dogrudan dosya yoluna ekleniyor
        public string ReadUserFile(string fileName)
        {
            string path = "C:\\ContosoData\\uploads\\" + fileName; // ../../ ile disari cikilabilir
            return File.ReadAllText(path);
        }
    }
}
