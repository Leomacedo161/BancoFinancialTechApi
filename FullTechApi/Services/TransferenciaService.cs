using FullTechApiDesafio.Data;
using FullTechApiDesafio.Interface;
using FullTechApiDesafio.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

public class TransferenciaService : ITransferenciaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly BancoContext _context;

    public TransferenciaService(IHttpClientFactory httpClientFactory, IMemoryCache cache, BancoContext context)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _context = context;
    }

    public async Task<bool> RealizarTransferencia(Transferencia transferencia)
    {
        if (await VerificarDiaUtil(transferencia.DataTransferencia))
        {
            var contaOrigem = await _context.Contas.FindAsync(transferencia.ContaOrigemId);
            var contaDestino = await _context.Contas.FindAsync(transferencia.ContaDestinoId);

            if (contaOrigem == null || contaDestino == null || contaOrigem.Saldo < transferencia.Valor)
            {
                return false;
            }

            contaOrigem.Saldo -= transferencia.Valor;
            contaDestino.Saldo += transferencia.Valor;

            transferencia.TransferenciaConcluida = true;
            _context.Transferencias.Add(transferencia);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    private async Task<bool> VerificarDiaUtil(DateTime data)
    {
        var cacheKey = $"feriados-{data.Year}";

        // Verifica se os feriados estão no cache
        if (!_cache.TryGetValue(cacheKey, out List<FeriadoResponse> feriados))
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync($"https://brasilapi.com.br/api/feriados/v1/{data.Year}");
            feriados = JsonConvert.DeserializeObject<List<FeriadoResponse>>(response);

            // Define opções de cache
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            // Armazena no cache
            _cache.Set(cacheKey, feriados, cacheEntryOptions);
        }

        // Verifica se a data é um feriado ou fim de semana
        return !feriados.Any(f => f.Date == data.ToString("yyyy-MM-dd"))
               && data.DayOfWeek != DayOfWeek.Saturday
               && data.DayOfWeek != DayOfWeek.Sunday;
    }
}

// Modelo apenas para deserializar a resposta da API BrasilAPI
public class FeriadoResponse
{
    public string Date { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}