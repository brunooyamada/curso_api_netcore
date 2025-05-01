using Api.Application.Controllers;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "N�o � poss�vel realizar o Update")]
        public async Task E_Possivel_Invocar_a_Controller_Update_BadRequest()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>())).ReturnsAsync(
                new MunicipioDtoUpdateResult
                {
                    Id = 1,
                    Nome = "S�o Paulo",
                    UpdateAt = DateTime.UtcNow,
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "� um campo obrigat�rio");

            var municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Id = 1,
                Nome = "S�o Paulo",
                CodIBGE = 1,
            };

            var result = await _controller.Put(municipioDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
