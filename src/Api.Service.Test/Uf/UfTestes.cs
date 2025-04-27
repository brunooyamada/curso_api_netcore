using Domain.Dtos.Uf;

namespace Api.Service.Test.Uf
{
    public class UfTestes
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public long IdUf { get; set; }

        public List<UfDto> listaUfDto = new List<UfDto>();
        public UfDto ufDto;

        public UfTestes()
        {
            IdUf = 1;
            Sigla = Faker.Address.UsState().Substring(1, 3);
            Nome = Faker.Address.UsState();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UfDto
                {
                    Id = i,
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    Nome = Faker.Address.UsState()
                };
                listaUfDto.Add(dto);
            }

            ufDto = new UfDto
            {
                Id = IdUf,
                Sigla = Sigla,
                Nome = Nome
            };
        }
    }
}
