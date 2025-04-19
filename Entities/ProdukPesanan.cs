namespace POS.Api.Entities
{
    public class ProdukPesanan
    {
        public int PesananId { get; set; }
        public Pesanan? Pesanan { get; set; }

        public int ProdukId { get; set; }
        public Produk? Produk { get; set; }

        public int Jumlah { get; set; }
    }
}
