using FullTechApiDesafio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FullTechApiDesafio.Interface;

namespace FullTechApiDesafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContasController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarConta(Conta conta)
        {
            await _contaService.CadastrarConta(conta);
            return Ok(conta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterConta(int id)
        {
            var conta = await _contaService.ObterConta(id);
            if (conta == null)
            {
                return NotFound();
            }

            return Ok(conta);
        }
    }
}