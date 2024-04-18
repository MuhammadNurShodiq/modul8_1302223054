using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        string lang;
        int threshold;
        int low_fee;
        int high_fee;
        List<string> methods;
        string confirmation;


        BankInput bankInput = new BankInput();

        
    }

    public class BankTransferConfig
    {
        public string lang { get; set; }
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set;}
        public List<string> methods { get; set; }
        public string en {  get; set; }
        public string id { get; set; }

        public BankTransferConfig() { }
        public BankTransferConfig(string lang, int threshold, int low_fee, int high_fee, List<string> methods, string en, string id)
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

    public class BankInput
    {
        public BankTransferConfig bankTransferConfig;
        public const String filepath = @"bank_transfer_config.json";

        public BankInput()
        {
            try
            {
                ReadConfig();
            }

            catch (Exception)
            {
                SetDefault();
                WriteNewConfig();
            }
        }

        private BankTransferConfig ReadConfig()
        {
            string jsonData = File.ReadAllText(filepath);
            bankTransferConfig = JsonSerializer.Deserialize<BankTransferConfig>(jsonData);
            return bankTransferConfig;
        }

        private void SetDefault()
        {
            bankTransferConfig = new BankTransferConfig("en", 25000000, 6500, 15000, ["RTO (real-time)", "SKN", "RTGS", "BI FAST"], "yes", "ya");
        }

        private void WriteNewConfig()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            string JsonString = JsonSerializer.Serialize(bankTransferConfig, options);
            File.WriteAllText(filepath, JsonString);
        }

        public void UbahData (string dataBaru)
        {
            bool dataValid = (dataBaru == "en" || dataBaru == "id");

            if (dataBaru == null || !dataValid)
            {
                throw new ArgumentException();
            }

            if (dataValid)
            {
                bankTransferConfig.lang = dataBaru;

                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                };

                string updateData = JsonSerializer.Serialize(bankTransferConfig, jsonSerializerOptions);
                File.WriteAllText (filepath, updateData);
            }
        }
    }
}