using Api.Application.Controllers;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarCreate
{
    public class Retorno_Update
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Não é possível realizar o Create")]
        public async Task E_Possivel_Invocar_a_Controller_Create_BadRequest()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Post(It.IsAny<MunicipioDtoCreate>())).ReturnsAsync(
                new MunicipioDtoCreateResult
                {
                    Id = 1,
                    Nome = "São Paulo",
                    CreateAt = DateTime.UtcNow,
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var municipioDtoCreate = new MunicipioDtoCreate
            {
                Nome = "São Paulo",
                CodIBGE = 1,
            };

            var result = await _controller.Post(municipioDtoCreate);
            Assert.True(result is CreatedResult);
        }
    }
}
