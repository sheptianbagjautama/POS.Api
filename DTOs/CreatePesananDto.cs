namespace POS.Api.DTOs
{
    public class CreatePesananDto
    {
        public int MejaId { get; set; }
        public List<ItemPesananDto> Items { get; set; } = new();
    }

    public class  ItemPesananDto
    {
        public int ProdukId { get; set; }
        public int Jumlah { get; set; }
    }
}
