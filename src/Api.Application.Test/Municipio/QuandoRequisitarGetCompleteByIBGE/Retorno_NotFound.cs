using Api.Application.Controllers;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetCompleteByIBGE
{
    public class Retorno_NotFound
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É possível Realizar o Get IBGE com NotFound")]
        public async Task E_Possivel_Invocar_a_Controller_Get_IBGE_NotFound()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.GetCompleteByIBGE(It.IsAny<int>())).ReturnsAsync((MunicipioDtoCompleto) null);

            _controller = new MunicipiosController(serviceMock.Object);
            
            var result = await _controller.GetCompleteByIBGE(0);
            Assert.True(result is NotFoundObjectResult);
        }
    }
}
