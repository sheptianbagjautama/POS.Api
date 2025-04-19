using POS.Api.Models;

namespace POS.Api.Entities
{
    public class Pesanan
    {
        public int Id { get; set; }
        public DateTime TanggalPesan { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int MejaId { get; set; }
        public Meja? Meja { get; set; }

        public ICollection<ProdukPesanan> ProdukPesanan { get; set; } = new List<ProdukPesanan>();
    }
}
