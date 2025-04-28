using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Moq;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGet : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Get")]
        public async Task E_Possivel_Executar_Metodo_Get()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Get(IdMunicipio)).ReturnsAsync(municipioDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdMunicipio);
            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.Equal(IdUf, result.UfId);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<long>())).Returns(Task.FromResult((MunicipioDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(IdMunicipio);
            Assert.Null(_record);
        }
    }
}
