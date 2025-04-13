using Api.Application.Controllers;
using Domain.Dtos.User;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_Deleted
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possível executar o Deleted.")]
        public async Task E_Possivel_Invocar_a_Controller_Delete()
        {
            var serviceMock = new Mock<IUserService>();
            
            serviceMock.Setup(m => m.Delete(It.IsAny<long>()))
                       .ReturnsAsync(true);

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.Delete(new long());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as bool?;
            Assert.NotNull(resultValue);
            Assert.True((Boolean) resultValue.Value);
        }
    }
}
