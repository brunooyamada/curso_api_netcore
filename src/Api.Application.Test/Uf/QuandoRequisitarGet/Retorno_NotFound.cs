using Api.Application.Controllers;
using Domain.Dtos.Uf;
using Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Uf.QuandoRequisitarGet
{
    public class Retorno_NotFound
    {
        private UfsController _controller;

        [Fact(DisplayName = "É possível realizar o Get de Ufs")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IUfService>();

            serviceMock.Setup(m => m.Get(It.IsAny<long>())).Returns(Task.FromResult((UfDto) null));

            _controller = new UfsController(serviceMock.Object);

            var result = await _controller.Get(1);
            Assert.True(result is NotFoundResult);
        }
    }
}
