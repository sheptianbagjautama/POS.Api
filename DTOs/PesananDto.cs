namespace POS.Api.DTOs
{
    public class PesananDto
    {
        public int Id { get; set; }
        public DateTime TanggalPesan { get; set; }
        public string? UserId { get; set; }
        public int MejaId { get; set; }
        public decimal TotalHarga { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? MetodePembayaran { get; set; }

        /**
         * new() berarti membuat instance baru dari List<ItemPesananReadDto>.
         * Jadi saat objek class ini dibuat, Items langsung memiliki list kosong siap pakai — tidak null.
         */
        public List<ItemPesananReadDto> Items { get; set; } = new();
    }

    public class ItemPesananReadDto
    {
        public int ProdukId { get; set; }
        public decimal HargaProduk { get; set; }
        public string NamaProduk { get; set; } = null!;
        public int Jumlah { get; set; }
    }
}
