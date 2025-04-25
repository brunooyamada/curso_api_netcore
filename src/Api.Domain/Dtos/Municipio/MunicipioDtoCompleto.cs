using Domain.Dtos.Uf;

namespace Domain.Dtos.Municipio
{
    public class MunicipioDtoCompleto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int CodIBGE { get; set; }

        public long UfId { get; set; }

        public UfDto Uf { get; set; }
    }
}
