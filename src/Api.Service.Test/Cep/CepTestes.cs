using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Domain.Dtos.Uf;

namespace Api.Service.Test.Cep
{
    public class CepTestes
    {
        public static long IdCep { get; set; }
        public static string Cep { get; set; }
        public static string CepAlterado { get; set; }
        public static string Logradouro { get; set; }
        public static string LogradouroAlterado { get; set; }
        public static string Numero { get; set; }
        public static string NumeroAlterado { get; set; }

        public static long IdMunicipio { get; set; }

        public MunicipioDtoCompleto Municipio { get; set; }

        public List<CepDto> listaDto = new List<CepDto>();
        public CepDto cepDto;
        public CepDtoCreate cepDtoCreate;
        public CepDtoCreateResult cepDtoCreateResult;
        public CepDtoUpdate cepDtoUpdate;
        public CepDtoUpdateResult cepDtoUpdateResult;

        public CepTestes()
        {
            IdCep = 1;
            IdMunicipio = 1;

            Cep = Faker.Address.ZipCode();
            Logradouro = Faker.Address.StreetName();
            Numero = Faker.Address.StreetAddress();
            
            CepAlterado = Faker.Address.ZipCode();
            LogradouroAlterado = Faker.Address.StreetName();
            NumeroAlterado = Faker.Address.StreetAddress();

            for (int i = 0; i < 10; i++)
            {
                var dto = new CepDto
                {
                    Id = i,
                    Cep = Faker.Address.ZipCode(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                    MunicipioId = Faker.RandomNumber.Next(1, 1000),
                    Municipio = new MunicipioDtoCompleto
                    {
                        Id = Faker.RandomNumber.Next(1, 1000),
                        Nome = Faker.Address.City(),
                        CodIBGE = Faker.RandomNumber.Next(1, 100000),
                        UfId = Faker.RandomNumber.Next(1, 27),
                        Uf = new UfDto
                        {
                            Id = Faker.RandomNumber.Next(1, 27),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)
                        }
                    }
                };

                listaDto.Add(dto);
            }

            cepDto = new CepDto
            {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = IdMunicipio,
            };

            cepDtoCreate = new CepDtoCreate
            {
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = IdMunicipio,
            };

            cepDtoCreateResult = new CepDtoCreateResult
            {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = IdMunicipio,
            };

            cepDtoUpdate = new CepDtoUpdate
            {
                Id = IdCep,
                Cep = CepAlterado,
                Logradouro = LogradouroAlterado,
                Numero = NumeroAlterado,
                MunicipioId = IdMunicipio,
            };

            cepDtoUpdateResult = new CepDtoUpdateResult
            {
                Id = IdCep,
                Cep = CepAlterado,
                Logradouro = LogradouroAlterado,
                Numero = NumeroAlterado,
                MunicipioId = IdMunicipio,
            };
        }
    }
}
