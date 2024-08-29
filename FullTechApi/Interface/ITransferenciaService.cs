using FullTechApiDesafio.Models;

namespace FullTechApiDesafio.Interface;

public interface ITransferenciaService
{
    Task<bool> RealizarTransferencia(Transferencia transferencia);
}