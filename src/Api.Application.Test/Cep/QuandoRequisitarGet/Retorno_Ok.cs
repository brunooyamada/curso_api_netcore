﻿using Api.Application.Controllers;
using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Cep.QuandoRequisitarGet
{
    public class Retorno_Ok
    {
        private CepsController _controller;

        [Fact(DisplayName = "É possível realizar o Get")]
        public async Task E_Possivel_Realizar_Get()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<long>())).ReturnsAsync(
               new CepDto
               {
                   Id = 1,
                   Cep = Faker.Address.ZipCode(),
                   Logradouro = Faker.Address.StreetName(),
                   Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                   MunicipioId = 1,
               }
            );

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.Get(1);
            Assert.True(result is OkObjectResult);
        }

        [Fact(DisplayName = "É possível realizar o Get by Cep")]
        public async Task E_Possivel_Realizar_GetByCep()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(m => m.Get(It.IsAny<string>())).ReturnsAsync(
               new CepDto
               {
                   Id = 1,
                   Cep = Faker.Address.ZipCode(),
                   Logradouro = Faker.Address.StreetName(),
                   Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                   MunicipioId = 1,
               }
            );

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.Get("123");
            Assert.True(result is OkObjectResult);
        }
    }
}
