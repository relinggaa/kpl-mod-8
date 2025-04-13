using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tpmodul8_103022300107
{
    class CovidConfig
    {
        public static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "covid_config.json");

        [JsonPropertyName("satuan_suhu")]
        public string SatuanSuhu { get; set; } = "celcius";
        [JsonPropertyName("batas_hari_demam")]
        public int BatasDemam { get; set; } = 14;
        [JsonPropertyName("pesan_ditolak")]
        public string PesanDitolak { get; set; } = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        [JsonPropertyName("pesan_diterima")]
        public string PesanDiterima { get; set; } = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

        public CovidConfig() { }

        public void UbahSatuan()
        {
       
            SatuanSuhu = SatuanSuhu == "celcius" ? "fahrenheit" : "celcius";
            SaveConfig();
        }

        public void LoadConfig()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    var configJson = File.ReadAllText(FilePath);
                    var configFromFile = JsonSerializer.Deserialize<CovidConfig>(configJson);
               
                    SatuanSuhu = configFromFile.SatuanSuhu;
                    BatasDemam = configFromFile.BatasDemam;
                    PesanDitolak = configFromFile.PesanDitolak;
                    PesanDiterima = configFromFile.PesanDiterima;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
                }
            }
            else
            {
                SaveConfig();
                Console.WriteLine("File tidak ditemukan, membuat file baru");
            }
        }

        public void SaveConfig()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(this, options);
                File.WriteAllText(FilePath, jsonString);
                Console.WriteLine("Berhasil menyimpan konfigurasi baru");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan konfigurasi baru: {ex.Message}");
            }
        }

        public string GetMessage(bool isRejected) =>
            isRejected ? PesanDitolak : PesanDiterima;
    }
}