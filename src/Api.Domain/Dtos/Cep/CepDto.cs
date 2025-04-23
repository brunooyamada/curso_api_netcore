using Domain.Dtos.Municipio;

namespace Domain.Dtos.Cep
{
    internal class CepDto
    {
        public long Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }

        public long MunicipioId { get; set; }

        public MunicipioDtoCompleto Municipio { get; set; }
    }
}
