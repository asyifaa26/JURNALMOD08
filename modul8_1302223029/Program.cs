using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class Program
{
    // Class Config berfungsi untuk menampung konfigurasi
    public class Config
    {
        // Atribute
        public string lang { get; set; }
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }
        public string methods { get; set; }
        public string en { get; set; }
        public string id { get; set; }


        // Constructor kosong untuk deserialisasi
        public Config() { }

        // Menerima masukan dari deserialisasi
        public Config(string lang, int threshold, int low_fee, int high_fee, string methods, string en, string id)
        {
            this.lang = lang;
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
            this.methods = methods;
            this.en = en;
            this.id = id;
        }
    }

    //  Class untuk membaca dan menulis file konfigurasi
    public class BankTransferConfig
    {
        // Deklarasi nama variabel configuration dengan tipe data Config
        public Config configuration;

        // Pada filePath ini SENGAJA tidak diarahkan ke file JSON dengan benar agar code Catch dibawah ini dijalankan
        // File Baru akan terbantuk dengan sendirinya dan dinamai sesuai nama file ujungnya yakni Covid_Config.json
        public const string filePath = "D:\\TELKOM\\SEMESTER 4\\PRATIKUM KPL\\modul8_1302223029\\modul8_1302223029\\bin\\Debug\\net8.0\\bank_transfer_config.json";

        // Method untuk membaca dan menulis file Covid_Config.json baru jika belum ada/dibuat
        public BankTransferConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }

        // Deklarasi nilai Default-nya dari BankTransferConfig
        public void SetDefault()
        {
            configuration = new Config("en", 25000000, 6500, 15000, " ", "yes", "ya");
            List<string> methods = new List<string>() { "RTO(real - time)", "SKN", "RTGS", "BI FAST" };
        }

        // Method untuk membaca file configurasi
        private Config ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            configuration = JsonSerializer.Deserialize<Config>(configJsonData);
            return configuration;
        }

        // Method untuk menulis file configurasi
        public void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            String jsonString = JsonSerializer.Serialize(configuration, options);
            File.WriteAllText(filePath, jsonString);
        }
    }

    public static void Main(string[] args)
    {
        // Memanggil Class BankTransferConfig untuk integrasi json, dan runtime configuration
        BankTransferConfig Konfig = new BankTransferConfig();

        Console.WriteLine("en/id");
        string input = Console.ReadLine();
        // Menerima inputan dalam satuan suhu integer
        if (input == "en")
        {
            Console.WriteLine("Please insert the amount of money to transfer: ");
            double inputTransfer = Convert.ToDouble(Console.ReadLine());

            int biayatf;
            if (inputTransfer <= Konfig.configuration.threshold)
            {
                biayatf = Konfig.configuration.low_fee;
            }
            else
            {
                biayatf = Konfig.configuration.high_fee;
            }

            Console.WriteLine("Transfer fee = " + biayatf + "Total amount = " + (inputTransfer + biayatf));

            Console.WriteLine("Select transfer method: ");

            Console.WriteLine("Please type " + Konfig.configuration.methods + " to confirm the transaction:");


        } else if (input == "id")
        {
            Console.WriteLine("Masukkan jumlah uang yang akan di-transfer: ");
            double inputTransfer = Convert.ToDouble(Console.ReadLine());

            int biayatf;
            if (inputTransfer <= Konfig.configuration.threshold)
            {
                biayatf = Konfig.configuration.low_fee;
            }
            else
            {
                biayatf = Konfig.configuration.high_fee;
            }

            Console.WriteLine("Biaya transfer = " + biayatf + "Total biaya = " + (inputTransfer + biayatf));
            Console.WriteLine("Pilih metode transfer ");
            Console.WriteLine("Ketik " + Konfig.configuration.methods + " untuk mengkonfirmasi transaksi")
        }

      


    }
}