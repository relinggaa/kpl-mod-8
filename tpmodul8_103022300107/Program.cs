using tpmodul8_103022300107;

class Program
{
    static void Main(string[] args)
    {
        var config = new CovidConfig();
        config.LoadConfig();

        Console.WriteLine("Selamat datang di aplikasi pemeriksaan Covid-19");
        Console.WriteLine($"Satuan suhu saat ini: {config.SatuanSuhu}");
        Console.Write("Apakah anda ingin mengubah satuan suhu? (Y/N) : ");
        if (Console.ReadLine().ToLower() == "y")
        {
            config.UbahSatuan();
            Console.WriteLine($"Satuan suhu diubah menjadi: {config.SatuanSuhu}");
        }

        Console.WriteLine("\nPertanyaan");
        Console.Write($"Berapa suhu badan anda saat ini? (dalam nilai {config.SatuanSuhu}): ");
        double suhu = Convert.ToDouble(Console.ReadLine());

        bool isValidTemperature = config.SatuanSuhu == "celcius"
            ? suhu >= 36.5 && suhu <= 37.5
            : suhu >= 97.7 && suhu <= 99.5;

        if (!isValidTemperature)
        {
            Console.WriteLine($"\n{config.GetMessage(true)}");
            return;
        }

        Console.Write("Berapa hari yang lalu anda terakhir memiliki gejala demam? : ");
        int hari = Convert.ToInt32(Console.ReadLine());

        if (hari <= config.BatasDemam)
        {
            Console.WriteLine($"\n{config.GetMessage(true)}");
        }
        else
        {
            Console.WriteLine($"\n{config.GetMessage(false)}");
        }
    }
}
