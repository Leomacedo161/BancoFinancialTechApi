using FullTechApiDesafio.Data;
using FullTechApiDesafio.Interface;
using FullTechApiDesafio.Models;

namespace FullTechApiDesafio.Services
{
    public class ContaService : IContaService
    {
        private readonly BancoContext _context;

        public ContaService(BancoContext context)
        {
            _context = context;
        }

        public virtual async Task CadastrarConta(Conta conta)
        {
            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<Conta> ObterConta(int id)
        {
            return await _context.Contas.FindAsync(id);
        }
    }
}