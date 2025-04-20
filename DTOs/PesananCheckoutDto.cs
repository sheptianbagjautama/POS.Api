namespace POS.Api.DTOs
{
    public class PesananCheckoutDto
    {
        public int PesananId { get; set; }
        public string MetodePembayaran { get; set; } = "Cash";
    }
}
