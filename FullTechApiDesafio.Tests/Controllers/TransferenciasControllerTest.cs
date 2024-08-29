using FullTechApiDesafio.Interface;
using FullTechApiDesafio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FullTechApiDesafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferenciasController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;

        public TransferenciasController(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarTransferencia(Transferencia transferencia)
        {
            var sucesso = await _transferenciaService.RealizarTransferencia(transferencia);
            if (sucesso)
            {
                return Ok("Transferência realizada com sucesso.");
            }
            else
            {
                return BadRequest("Transferência falhou (verifique os feriados e dias úteis).");
            }
        }
    }
}