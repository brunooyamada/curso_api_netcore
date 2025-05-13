using Domain.Dtos.Municipio;
using Domain.Dtos.ViaCep;
using Domain.Interfaces.Services.Municipio;
using Moq;

namespace Api.Service.Test.Municipio
{
    public class QuantoForExecutadoGetOrCreateMunicipio : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Create")]
        public async Task E_Possivel_Executar_Metodo_Create()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetOrCreateMunicipio(viaCepDto, IdUf)).ReturnsAsync(municipioDtoCompleto);
            _service = _serviceMock.Object;

            var result = await _service.GetOrCreateMunicipio(viaCepDto, IdUf);
            Assert.NotNull(result);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.Equal(IdUf, result.UfId);
        }
    }
}
