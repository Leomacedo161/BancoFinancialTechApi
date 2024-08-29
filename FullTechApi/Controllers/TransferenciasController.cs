using FullTechApiDesafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullTechApiDesafio.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TransferenciasController : ControllerBase
{
    private readonly TransferenciaService _service;

    public TransferenciasController(TransferenciaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> RealizarTransferencia([FromBody] Transferencia transferencia)
    {
        var sucesso = await _service.RealizarTransferencia(transferencia);
        if (!sucesso)
        {
            return BadRequest("Transferência falhou (verifique os feriados e dias úteis).");
        }

        return Ok("Transferência realizada com sucesso.");
    }
}