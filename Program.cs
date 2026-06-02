public class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _isAvailable;

    public Stand(string namaStand, double hargaSewaPerHari)
    {
        _namaStand = namaStand;
        _hargaSewaPerHari = hargaSewaPerHari;
        _isAvailable = true;
    }

 
    public string NamaStand
    {
        get { return _namaStand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nama stand tidak boleh kosong atau hanya spasi.");
            _namaStand = value;
        }
    }


    public double HargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Harga sewa per hari harus lebih besar dari 0.");
            _hargaSewaPerHari = value;
        }
    }

 
    public bool IsAvailable
    {
        get { return _isAvailable; }
    }

  
    public void DisplayInfo()
    {
        string status = _isAvailable ? "Tersedia" : "Tidak tersedia";
        Console.WriteLine($"Nama Stand : {_namaStand}");
        Console.WriteLine($"Harga/Hari : Rp{_hargaSewaPerHari:N0}");
        Console.WriteLine($"Status     : {status}");
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