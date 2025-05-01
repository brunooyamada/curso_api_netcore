using Api.Application.Controllers;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarGet
{
    public class Retorno_NotFound
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É possível Realizar o Get com NotFound")]
        public async Task E_Possivel_Invocar_a_Controller_Get_NotFound()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Get(It.IsAny<long>())).ReturnsAsync((MunicipioDto) null);

            _controller = new MunicipiosController(serviceMock.Object);
            
            var result = await _controller.Get(0);
            Assert.True(result is NotFoundObjectResult);
        }
    }
}
