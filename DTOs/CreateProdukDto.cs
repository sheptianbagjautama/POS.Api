namespace POS.Api.DTOs
{
    public class CreateProdukDto
    {
        public required string Nama { get; set; }
        public required decimal Harga { get; set; }
        public required int Stok { get; set; }
        public string? GambarUrl { get; set; }
        public required int KategoriId { get; set; }
    }
}
