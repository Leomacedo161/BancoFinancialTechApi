using FullTechApiDesafio.Data;
using FullTechApiDesafio.Interface;
using FullTechApiDesafio.Models;
using Microsoft.EntityFrameworkCore;

namespace FullTechApiDesafio.Services;

public class ExtratoService : IExtratoService
{
    private readonly BancoContext _context;

    public ExtratoService(BancoContext context)
    {
        _context = context;
    }

    public async Task<Extrato> GerarExtrato(int contaId, DateTime dataInicio, DateTime dataFim)
    {
        var conta = await _context.Contas.FindAsync(contaId);
        if (conta == null)
        {
            throw new ArgumentException("Conta não encontrada.");
        }

        if (dataInicio == DateTime.MinValue || dataFim == DateTime.MinValue)
        {
            throw new ArgumentException("Período do extrato não informado.");
        }

        if (dataInicio > dataFim)
        {
            throw new ArgumentException("A data de início não pode ser posterior à data de fim.");
        }

        dataInicio = DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc);
        dataFim = DateTime.SpecifyKind(dataFim, DateTimeKind.Utc);

        var transferencias = await _context.Transferencias
            .Where(t => (t.ContaOrigemId == contaId || t.ContaDestinoId == contaId) 
                        && t.DataTransferencia >= dataInicio 
                        && t.DataTransferencia <= dataFim)
            .ToListAsync();

        return new Extrato
        {
            ContaId = contaId,
            DataInicio = dataInicio,
            DataFim = dataFim,
            Transferencias = transferencias
        };
    }
}