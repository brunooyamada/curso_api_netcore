using Api.Application.Controllers;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarDelete
{
    public class Retorno_Deleted
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "� poss�vel Realizar o Deleted")]
        public async Task E_Possivel_Invocar_a_Controller_Deleted()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<long>()))
                .ReturnsAsync(true);

            _controller = new MunicipiosController(serviceMock.Object);

            var result = await _controller.Delete(1);
            Assert.True(result is OkObjectResult);
        }
    }
}