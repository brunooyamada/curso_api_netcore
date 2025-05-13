using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Moq;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoGet : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Get")]
        public async Task E_Possivel_Executar_Get()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(IdCep)).ReturnsAsync(cepDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdCep);
            Assert.NotNull(result);
            Assert.True(result.Id == IdCep);
            Assert.Equal(Cep, result.Cep);
            Assert.Equal(Logradouro, result.Logradouro);
            Assert.Equal(Numero, result.Numero);
            Assert.Equal(IdMunicipio, result.MunicipioId);
            Assert.Equal(Municipio, result.Municipio);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<long>())).ReturnsAsync((CepDto)null);
            _service = _serviceMock.Object;

            result = await _service.Get(IdCep);
            Assert.Null(result);

            // Testando o método Get com Cep
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(Cep)).ReturnsAsync(cepDto);
            _service = _serviceMock.Object;

            result = await _service.Get(Cep);
            Assert.NotNull(result);
            Assert.Equal(Cep, result.Cep);
            Assert.Equal(Logradouro, result.Logradouro);
            Assert.Equal(Numero, result.Numero);
            Assert.Equal(IdMunicipio, result.MunicipioId);
            Assert.Equal(Municipio, result.Municipio);

            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<string>())).ReturnsAsync((CepDto)null);
            _service = _serviceMock.Object;

            result = await _service.Get(IdCep);
            Assert.Null(result);
        }

        [Fact(DisplayName = "É possível executar o método GetByApi")]
        public async Task TesteGetById()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.GetByApi(Cep)).ReturnsAsync(cepDto);
            _service = _serviceMock.Object;

            var result = await _service.GetByApi(Cep);
            Assert.NotNull(result);
            Assert.True(result.Id == IdCep);
        }

    }
}
