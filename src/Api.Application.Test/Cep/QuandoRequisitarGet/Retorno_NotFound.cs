using Api.Application.Controllers;
using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Cep.QuandoRequisitarGet
{
    public class Retorno_NotFound
    {
        private CepController _controller;

        [Fact(DisplayName = "É possível realizar o Get")]
        public async Task E_Possivel_Realizar_Get()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<long>())).ReturnsAsync((CepDto) null);

            _controller = new CepController(serviceMock.Object);
            
            var result = await _controller.Get(1);
            Assert.True(result is NotFoundObjectResult);
        }

        [Fact(DisplayName = "É possível realizar o Get by Cep")]
        public async Task E_Possivel_Realizar_GetByCep()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<string>())).ReturnsAsync((CepDto)null);

            _controller = new CepController(serviceMock.Object);

            var result = await _controller.Get("123");
            Assert.True(result is NotFoundObjectResult);
        }
    }
}
