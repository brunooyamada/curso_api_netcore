using Domain.Interfaces.Services.Cep;
using Moq;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoUpdate : CepTestes
    {
        private ICepService _service;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Update")]
        public async Task E_Possivel_Executar_Medoto_Update()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Put(cepDtoUpdate)).ReturnsAsync(cepDtoUpdateResult);
            _service = _serviceMock.Object;

            var result = await _service.Put(cepDtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(IdCep, result.Id);
            Assert.Equal(CepAlterado, result.Cep);
            Assert.Equal(LogradouroAlterado, result.Logradouro);
            Assert.Equal(NumeroAlterado, result.Numero);
            Assert.Equal(IdMunicipio, result.MunicipioId);
        }
    }
}
