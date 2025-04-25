namespace Domain.Dtos.Municipio
{
    public class MunicipioDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int CodIBGE { get; set; }

        public long UfId { get; set; }
    }
}
