using Domain.Interfaces.Services.Municipio;
using Moq;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteById : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GetCompleteById")]
        public async Task E_Possivel_Executar_Metodo_GetCompleteById()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompleteById(IdMunicipio)).ReturnsAsync(municipioDtoCompleto);
            _service = _serviceMock.Object;

            var _result = await _service.GetCompleteById(IdMunicipio);
            Assert.NotNull(_result);
            Assert.True(_result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, _result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, _result.CodIBGE);
            Assert.NotNull(_result.Uf);
        }
    }
}
