﻿using Api.Application.Controllers;
using Domain.Dtos.User;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Usuario.QuandoRequisitarCreate
{
    public class Retorno_Created
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possível executar o Created.")]
        public async Task E_Possivel_Invocar_a_Controller_Create()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(
                new UserDtoCreateResult
                {
                    Id = 1,
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow,
                }
            );

            _controller = new UsersController(serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var userDtoCreate = new UserDtoCreate
            {
                Name = nome,
                Email = email,
            };

            var result = await _controller.Post(userDtoCreate);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as UserDtoCreateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(userDtoCreate.Name, resultValue.Name);
            Assert.Equal(userDtoCreate.Email, resultValue.Email);
        }
    }
}
