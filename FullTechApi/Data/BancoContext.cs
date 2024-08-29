using FullTechApiDesafio.Models;
using Microsoft.EntityFrameworkCore;

namespace FullTechApiDesafio.Data;

public class BancoContext : DbContext
{
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Transferencia> Transferencias { get; set; }

    public BancoContext(DbContextOptions<BancoContext> options) : base(options)
    {
    }
}