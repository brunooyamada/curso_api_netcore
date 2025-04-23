using System;

namespace Domain.Dtos.Municipio
{
    internal class MunicipioDtoUpdateResult
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int CodIBGE { get; set; }
        public long UfId { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
