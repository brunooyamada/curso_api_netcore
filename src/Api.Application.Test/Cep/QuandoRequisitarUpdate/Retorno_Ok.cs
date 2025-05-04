using Api.Application.Controllers;
using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Cep.QuandoRequisitarUpdate
{
    public class Retorno_Ok
    {
        private CepsController _controller;

        [Fact(DisplayName = "É possível realizar o Update")]
        public async Task E_Possivel_Realizar_Update()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Put(It.IsAny<CepDtoUpdate>())).ReturnsAsync(
                new CepDtoUpdateResult
                {
                    Id = 1,
                    Cep = Faker.Address.ZipCode(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                    MunicipioId = 1,
                    UpdateAt = DateTime.UtcNow,
                }
            );

            _controller = new CepsController(serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var cepDtoCreate = new CepDtoUpdate
            {
                Id = 1,
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                MunicipioId = 1,
            };

            var result = await _controller.Put(cepDtoCreate);
            Assert.True(result is OkObjectResult);
        }
    }
}
