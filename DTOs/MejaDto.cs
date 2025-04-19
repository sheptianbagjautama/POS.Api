namespace POS.Api.DTOs
{
    public class MejaDto
    {
        public int Id { get; set; }
        public required string Nomor { get; set; }
        public bool IsTersedia { get; set; }
    }
}
