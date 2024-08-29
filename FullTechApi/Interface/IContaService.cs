using FullTechApiDesafio.Models;

namespace FullTechApiDesafio.Interface;

public interface IContaService
{
    Task CadastrarConta(Conta conta);
    Task<Conta> ObterConta(int id);
}