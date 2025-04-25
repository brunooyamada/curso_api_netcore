using Domain.Dtos.Uf;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Municipio
{
    public class MunicipioDtoCreate
    {
        [Required(ErrorMessage = "Nome de Município é campo obrigatório")]
        [StringLength(60, ErrorMessage = "Nome de Município deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Código do IBGE inválido")]
        public int CodIBGE { get; set; }
        [Required(ErrorMessage = "Códigio de Uf é campo obrigatório")]
        public long UfId { get; set; }

    }
}
