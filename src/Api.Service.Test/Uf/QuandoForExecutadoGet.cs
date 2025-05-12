using Domain.Dtos.Uf;
using Domain.Interfaces.Services.Uf;
using Moq;

namespace Api.Service.Test.Uf
{
    public class QuandoForExecutadoGet : UfTestes
    {
        private IUfService _service;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "É possível executar o método Get")]
        public async Task E_Possivel_Executar_Metodo_Get ()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.Get(IdUf)).ReturnsAsync(ufDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdUf);
            Assert.NotNull(result);
            Assert.True(result.Id == IdUf);
            Assert.Equal(Nome, result.Nome);

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<long>())).Returns(Task.FromResult((UfDto)null));
            _service = _serviceMock.Object;

            var record = await _service.Get(IdUf);
            Assert.Null(record);
        }

        [Fact(DisplayName = "É possível executar o método GetPorSigla")]
        public async Task Get_PorSigla()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetPorSigla(Sigla)).ReturnsAsync(ufDto);
            _service = _serviceMock.Object;

            var result = await _service.GetPorSigla(Sigla);
            Assert.NotNull(result);
            Assert.True(result.Sigla == Sigla);
        }
    }
}
