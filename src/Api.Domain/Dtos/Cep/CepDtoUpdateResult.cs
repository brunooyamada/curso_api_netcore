using System;

namespace Domain.Dtos.Cep
{
    internal class CepDtoUpdateResult
    {
        public long Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public long MunicipioId { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
