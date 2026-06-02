List<Stand> daftarStand = new List<Stand>()
{
    new StandOutdoor("Outdoor-1", 450000),
    new StandOutdoor("Outdoor-2", 500000),
    new StandIndoor("Indoor-1", 600000),
    new StandIndoor("Indoor-2", 700000),
    new StandPremium("Premium-1", 1800000),
    new StandPremium("Premium-2", 2000000)
};

bool berjalan = true;

while (berjalan)
{
    Console.Clear();
    Console.WriteLine("=== Moklet Expo Management Center ===");
    Console.WriteLine("Daftar Stand Tersedia");
    foreach (var stand in daftarStand)
    {
        if (stand.IsAvailable)
        {
            stand.DisplayInfo();
        }
    }
    Console.WriteLine("\n1. Sewa Stand");
    Console.WriteLine("2. Akhiri Sewa Stand");
    Console.WriteLine("3. Keluar");
    Console.Write("\nPilih Menu: ");
    string pilihan = Console.ReadLine();

    if (pilihan == "1")
    {
        Console.Write("\nMasukkan nama stand: ");
        string namaInput = Console.ReadLine();
        Stand standDicari = daftarStand.FirstOrDefault(s => s.NamaStand.Equals(namaInput, StringComparison.OrdinalIgnoreCase));

        if (standDicari == null)
        {
            Console.WriteLine("Stand tidak ditemukan.");
        }
        else if (!standDicari.IsAvailable)
        {
            Console.WriteLine("Stand sedang tidak tersedia.");
        }
        else
        {
            Console.WriteLine($"Stand ditemukan: {standDicari.NamaStand} | Rp {standDicari.HargaSewaPerHari} / hari");
            Console.Write("Masukkan jumlah hari: ");
            if (int.TryParse(Console.ReadLine(), out int hari) && hari > 0)
            {
                double totalBiaya = standDicari.HitungTotal(hari);
                Console.WriteLine($"\nTotal Biaya: Rp {totalBiaya}");
                standDicari.UbahStatus();
                Console.WriteLine($"Stand {standDicari.NamaStand} berhasil disewakan selama {hari} hari");
            }
            else
            {
                Console.WriteLine("Jumlah hari tidak valid.");
            }
        }
        Console.WriteLine("\nTekan ENTER untuk melanjutkan");
        Console.ReadLine();
        continue;
    }
    else if (pilihan == "2")
    {
        Console.WriteLine("\nDaftar Stand yang Sedang Disewakan");
        bool adaSewa = false;
        foreach (var stand in daftarStand)
        {
            if (!stand.IsAvailable)
            {
                stand.DisplayInfo();
                adaSewa = true;
            }
        }
        Console.Write("\nMasukkan nama stand: ");
        string namaInput = Console.ReadLine();
        Stand standDicari = daftarStand.FirstOrDefault(s => s.NamaStand.Equals(namaInput, StringComparison.OrdinalIgnoreCase));

        if (standDicari == null)
        {
            Console.WriteLine("Stand tidak ditemukan.");
        }
        else if (standDicari.IsAvailable)
        {
            Console.WriteLine("Stand belum disewa.");
        }
        else
        {
            standDicari.UbahStatus();
            Console.WriteLine($"\nSewa stand {standDicari.NamaStand} berhasil diakhiri");
        }
        Console.WriteLine("\nTekan ENTER untuk melanjutkan");
        Console.ReadLine();
        continue;
    }
    else if (pilihan == "3")
    {
        Console.WriteLine("\nTerima kasih...");
        berjalan = false;
        Console.WriteLine("\nTekan ENTER untuk mengakhiri");
        Console.ReadLine();
        break;
    }
    else
    {
        Console.WriteLine("\nPilihan menu tidak tersedia.");
        Console.WriteLine("\nTekan ENTER untuk melanjutkan");
        Console.ReadLine();
        continue;
    }
}
public class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _isAvailable;

    public Stand(string nama_stand, double harga_sewa_per_hari)
    {
        _namaStand = nama_stand;
        _hargaSewaPerHari = harga_sewa_per_hari;
        _isAvailable = true;
    }
    public string NamaStand
    {
        get { return _namaStand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Nama stand tidak boleh kosong atau hanya berisi spasi!");
            }
            else
            {
                _namaStand = value;
            }
        }
    }
    public double HargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            if (value > 0)
            {
                _hargaSewaPerHari = value;
            }
            else
            {
                Console.WriteLine("Harga sewa harus lebih besar dari 0!");
            }
        }
    }
    public bool IsAvailable
    {
        get { return _isAvailable; }
    }
    public void DisplayInfo()
    {
        string statusStr = _isAvailable ? "Tersedia" : "Tidak tersedia";
        Console.WriteLine($"{_namaStand,-15} | Rp {_hargaSewaPerHari + " / Hari",-14} | {statusStr}");
    }
    public void UbahStatus()
    {
        _isAvailable = !_isAvailable;
    }
    public virtual double HitungTotal(int jumlahHari)
    {
        return _hargaSewaPerHari * jumlahHari;
    }
}

    public class StandOutdoor : Stand
    {
        protected double _biayaTenda;
        public StandOutdoor(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
        {
            _biayaTenda = 75000;
        }
        public double BiayaTenda
        {
            get { return _biayaTenda; }
        }
        public override double HitungTotal(int jumlahHari)
        {
            return (base._hargaSewaPerHari * jumlahHari) + (_biayaTenda * jumlahHari);
        }
    }
    public class StandIndoor : Stand
    {
        protected double _biayaListrik;
        public StandIndoor(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
        {
            _biayaListrik = 100000;
        }
        public double BiayaListrik
        {
            get { return _biayaListrik; }
        }
        public override double HitungTotal(int jumlahHari)
        {
            return (base._hargaSewaPerHari * jumlahHari) + (_biayaListrik * jumlahHari);
        }
    }
    public class StandPremium : Stand
    {
        protected double _biayaKeamanan;
        public StandPremium(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
        {
            _biayaKeamanan = 300000;
        }
        public double BiayaKeamanan
        {
            get { return _biayaKeamanan; }
        }
        public override double HitungTotal(int jumlahHari)
        {
            return (base._hargaSewaPerHari * jumlahHari) + _biayaKeamanan;
        }
    }