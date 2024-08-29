namespace FullTechApiDesafio.Models;

public class Extrato
{
    public int ContaId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public List<Transferencia> Transferencias { get; set; }
}
