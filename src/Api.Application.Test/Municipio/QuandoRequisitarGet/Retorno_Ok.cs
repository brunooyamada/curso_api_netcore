using Api.Application.Controllers;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarGet
{
    public class Retorno_Ok
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É possível Realizar o Get")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Get(It.IsAny<long>()))
                .ReturnsAsync(
                new MunicipioDto
                {
                    Id = 1,
                    Nome = "São Paulo",
                    CodIBGE = 1,
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            
            var result = await _controller.Get(1);
            Assert.True(result is OkObjectResult);
        }
    }
}