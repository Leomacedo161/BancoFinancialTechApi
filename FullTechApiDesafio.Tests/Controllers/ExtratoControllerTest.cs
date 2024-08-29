using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullTechApiDesafio.Controllers;
using FullTechApiDesafio.Interface;
using FullTechApiDesafio.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FullTechApiDesafio.Tests.Controllers
{
    public class ExtratoControllerTest
    {
        [Fact]
        public async Task GerarExtrato_ReturnsOkResult_WithValidContaIdAndDateRange()
        {
            var mockService = new Mock<IExtratoService>();
            var extrato = new Extrato
            {
                ContaId = 1,
                DataInicio = DateTime.UtcNow.AddMonths(-1),
                DataFim = DateTime.UtcNow,
                Transferencias = new List<Transferencia>()
            };
            mockService.Setup(s => s.GerarExtrato(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(extrato);
            var controller = new ExtratoController(mockService.Object);

            var result = await controller.GerarExtrato(1, DateTime.Now.AddDays(-10), DateTime.Now);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(extrato, okResult.Value);
        }

        [Fact]
        public async Task GerarExtrato_ReturnsNotFoundResult_WithNonExistingContaId()
        {
            var mockService = new Mock<IExtratoService>();
            mockService.Setup(s => s.GerarExtrato(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((Extrato)null);
            var controller = new ExtratoController(mockService.Object);

            var result = await controller.GerarExtrato(1, DateTime.Now.AddDays(-10), DateTime.Now);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GerarExtrato_ReturnsBadRequestResult_WithInvalidDateRange()
        {
            var mockService = new Mock<IExtratoService>();
            var controller = new ExtratoController(mockService.Object);

            var result = await controller.GerarExtrato(1, DateTime.Now, DateTime.Now.AddDays(-10));

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
