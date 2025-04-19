namespace POS.Api.Entities
{
    public class Meja
    {
        public int Id { get; set; }
        public required string Nomor { get; set; }
        public bool IsTersedia { get; set; }
    }
}
