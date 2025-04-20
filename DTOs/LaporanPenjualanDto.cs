namespace POS.Api.DTOs
{
    public class LaporanPenjualanDto
    {
        public int TotalPesanan { get; set; }
        public decimal TotalPendapatan { get; set; }
        public int TotalProdukTerjual { get; set; }
        public DateTime DariTanggal { get; set; }
        public DateTime SampaiTanggal { get; set; }
        public string NamaKasir { get; set; }
    }
}
