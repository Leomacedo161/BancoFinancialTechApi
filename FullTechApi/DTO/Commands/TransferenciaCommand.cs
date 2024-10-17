namespace FullTechApiDesafio.DTO.Commands
{
    public class TransferenciaCommand
    {
        public int ContaOrigemId { get; set; }
        public int ContaDestinoId { get; set; }
        public decimal Valor { get; set; }
    }
}
