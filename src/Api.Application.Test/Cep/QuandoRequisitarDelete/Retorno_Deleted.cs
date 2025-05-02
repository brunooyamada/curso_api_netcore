using Api.Application.Controllers;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Cep.QuandoRequisitarDelete
{
    public class Retorno_Deleted
    {
        private CepController _controller;

        [Fact(DisplayName = "É possível realizar o Delete")]
        public async Task E_Possivel_Realizar_Delete()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<long>())).ReturnsAsync(true);

            _controller = new CepController(serviceMock.Object);

            var result = await _controller.Delete(1);
            Assert.True(result is OkObjectResult);
        }
    }
}
