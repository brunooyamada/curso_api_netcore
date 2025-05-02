using Api.Application.Controllers;
using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Cep.QuandoRequisitarCreate
{
    public class Retorno_Created
    {
        private CepController _controller;

        [Fact(DisplayName = "É possível realizar o Create")]
        public async Task E_Possivel_Invocar_a_Controller_Create()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Post(It.IsAny<CepDtoCreate>())).ReturnsAsync(
                new CepDtoCreateResult
                {
                    Id = 1,
                    Cep = Faker.Address.ZipCode(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                    MunicipioId = 1,
                }
            );

            _controller = new CepController(serviceMock.Object);
            
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var cepDtoCreate = new CepDtoCreate
            {
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                MunicipioId = 1,
            };

            var result = await _controller.Post(cepDtoCreate);
            Assert.True(result is CreatedResult);
        }
    }
}
