using FullTechApiDesafio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FullTechApiDesafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtratoController : ControllerBase
    {
        private readonly IExtratoService _extratoService;

        public ExtratoController(IExtratoService extratoService)
        {
            _extratoService = extratoService;
        }

        [HttpGet]
        public async Task<IActionResult> GerarExtrato(int contaId, DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio > dataFim)
            {
                return BadRequest();
            }

            var extrato = await _extratoService.GerarExtrato(contaId, dataInicio, dataFim);
            if (extrato == null)
            {
                return NotFound();
            }

            return Ok(extrato);
        }
    }
}