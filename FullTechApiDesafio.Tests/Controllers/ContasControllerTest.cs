using System.Threading.Tasks;
using FullTechApiDesafio.Controllers;
using FullTechApiDesafio.Models;
using FullTechApiDesafio.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FullTechApiDesafio.Tests.Controllers
{
    public class ContasControllerTest
    {
        [Fact]
        public async Task CadastrarConta_ReturnsOkResult_WithValidConta()
        {
            var mockService = new Mock<IContaService>();
            var controller = new ContasController(mockService.Object);
            var novaConta = new Conta { Id = 1, NumeroConta = "123456", Saldo = 2500 };

            mockService.Setup(s => s.CadastrarConta(It.IsAny<Conta>())).Returns(Task.CompletedTask);

            var result = await controller.CadastrarConta(novaConta);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(novaConta, okResult.Value);
        }

        [Fact]
        public async Task ObterConta_ReturnsOkResult_WithExistingId()
        {
            var mockService = new Mock<IContaService>();
            var conta = new Conta { Id = 1, NumeroConta = "123456", Saldo = 2500 };
            mockService.Setup(s => s.ObterConta(It.IsAny<int>())).ReturnsAsync(conta);
            var controller = new ContasController(mockService.Object);

            var result = await controller.ObterConta(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(conta, okResult.Value);
        }

        [Fact]
        public async Task ObterConta_ReturnsNotFoundResult_WithNonExistingId()
        {
            var mockService = new Mock<IContaService>();
            mockService.Setup(s => s.ObterConta(It.IsAny<int>())).ReturnsAsync((Conta)null);
            var controller = new ContasController(mockService.Object);

            var result = await controller.ObterConta(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}