using Domain.Interfaces.Services.Municipio;
using Moq;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteByIBGE : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método GetCompleteByIBGE")]
        public async Task E_Possivel_Executar_Metodo_GetCompleteByIBGE()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetCompleteByIBGE(CodigoIBGEMunicipio)).ReturnsAsync(municipioDtoCompleto);
            _service = _serviceMock.Object;

            var _result = await _service.GetCompleteByIBGE(CodigoIBGEMunicipio);
            Assert.NotNull(_result);
            Assert.True(_result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, _result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, _result.CodIBGE);
            Assert.NotNull(_result.Uf);
        }
    }
}
