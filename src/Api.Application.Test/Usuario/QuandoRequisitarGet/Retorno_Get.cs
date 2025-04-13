﻿using Api.Application.Controllers;
using Domain.Dtos.User;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Usuario.QuandoRequisitarGet
{
    public class Retorno_Get
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possível realizar o Get")]
        public async Task E_Possivel_Executar_Get()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Get(It.IsAny<long>())).ReturnsAsync(
                new UserDto
                {
                    Id = 1,
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.Get(1);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UserDto;
            Assert.NotNull(resultValue);
            Assert.Equal(nome, resultValue.Name);
            Assert.Equal(email, resultValue.Email);
        }
    }
}
