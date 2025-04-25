using Domain.Dtos.Municipio;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Cep
{
    public class CepDtoCreate
    {
        [Required(ErrorMessage = "CEP é campo obrigatório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é campo obrigatório")]
        public string Logradouro { get; set; }

        public string Numero { get; set; }

        [Required(ErrorMessage = "Município é campo obrigatório")]
        public long MunicipioId { get; set; }
    }
}
