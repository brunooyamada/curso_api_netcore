using Api.Application.Controllers;
using Domain.Dtos.Uf;
using Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Uf.QuandoRequisitarGetAll
{
    public class Retorno_GetAll
    {
        private UfsController _controller;

        [Fact(DisplayName = "É possível realizar o GetAll de Ufs")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUfService>();

            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<UfDto>()
                {
                    new UfDto
                    {
                        Id = 1,
                        Nome = "São Paulo",
                        Sigla = "SP"
                    },
                    new UfDto
                    {
                        Id = 2,
                        Nome = "Rio de Janeiro",
                        Sigla = "RJ"
                    }
                }
            );

            _controller = new UfsController(serviceMock.Object);
            
            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);
        }
    }
}
