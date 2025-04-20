namespace POS.Api.DTOs
{
    public class ProdukTerlarisDto
    {
        public int ProdukId { get; set; }
        public string NamaProduk { get; set; } = string.Empty;
        public int TotalTerjual { get; set; }
    }
}
