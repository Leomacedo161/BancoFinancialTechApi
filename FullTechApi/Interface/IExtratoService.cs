using FullTechApiDesafio.Models;

namespace FullTechApiDesafio.Interface;

public interface IExtratoService
{
    Task<Extrato> GerarExtrato(int contaId, DateTime dataInicio, DateTime dataFim);
}