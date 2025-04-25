using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Cep
{
    public class CepDtoUpdate
    {
        [Required(ErrorMessage = "Id é campo obrigatório")]
        public long Id { get; set; }

        [Required(ErrorMessage = "CEP é campo obrigatório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é campo obrigatório")]
        public string Logradouro { get; set; }

        public string Numero { get; set; }

        [Required(ErrorMessage = "Município é campo obrigatório")]
        public long MunicipioId { get; set; }
    }
}
