namespace POS.Api.Entities
{
    public class Produk
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public decimal Harga { get; set; } = 0m;
        public int Stok { get; set; }
        public string? GambarUrl { get; set; }

        //Relasi ke kategori
        public int KategoriId { get; set; }
        public Kategori? Kategori { get; set; }
    }
}
