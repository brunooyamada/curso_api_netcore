using System;

namespace Domain.Dtos.Municipio
{
    internal class MunicipioDtoCreateResult
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int CodIBGE { get; set; }
        public long UfId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
