using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Cep
{
    public class CepDtoCreateResult
    {
        public long Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public long MunicipioId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
