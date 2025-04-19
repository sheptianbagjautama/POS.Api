namespace POS.Api.DTOs
{
    public class ProdukDto
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public string? GambarUrl { get; set; }
        public int KategoriId { get; set; }
    }
}
