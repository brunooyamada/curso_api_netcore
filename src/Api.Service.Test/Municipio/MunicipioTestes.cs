﻿using Domain.Dtos.Municipio;
using Domain.Dtos.Uf;
using Domain.Dtos.ViaCep;

namespace Api.Service.Test.Municipio
{
    public class MunicipioTestes
    {
        public static string NomeMunicipio { get; set; }
        public static int CodigoIBGEMunicipio { get; set; }
        public static string NomeMunicipioAlterado { get; set; }
        public static int CodigoIBGEMunicipioAlterado { get; set; }
        public static long IdMunicipio { get; set; }
        public static long IdUf { get; set; }

        public List<MunicipioDto> listaDto = new List<MunicipioDto>();
        public MunicipioDto municipioDto;
        public MunicipioDtoCompleto municipioDtoCompleto;
        public MunicipioDtoCreate municipioDtoCreate;
        public MunicipioDtoCreateResult municipioDtoCreateResult;
        public MunicipioDtoUpdate municipioDtoUpdate;
        public MunicipioDtoUpdateResult municipioDtoUpdateResult;
        public ViaCepDto viaCepDto;

        public MunicipioTestes()
        {
            IdMunicipio = 1;
            NomeMunicipio = Faker.Address.City();
            CodigoIBGEMunicipio = Faker.RandomNumber.Next(1, 100000);
            NomeMunicipioAlterado = Faker.Address.City();
            CodigoIBGEMunicipioAlterado = Faker.RandomNumber.Next(1, 100000);
            IdUf = Faker.RandomNumber.Next(1, 27);

            for (int i = 0; i < 10; i++)
            {
                var dto = new MunicipioDto
                {
                    Id = i,
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 100000),
                    UfId = Faker.RandomNumber.Next(1, 27)
                };

                listaDto.Add(dto);
            }

            municipioDto = new MunicipioDto
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = IdUf
            };

            municipioDtoCompleto = new MunicipioDtoCompleto
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = IdUf,
                Uf = new UfDto
                {
                    Id = Faker.RandomNumber.Next(1, 27),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                }
            };

            municipioDtoCreate = new MunicipioDtoCreate
            {
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = IdUf
            };

            municipioDtoCreateResult = new MunicipioDtoCreateResult
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = IdUf,
                CreateAt = DateTime.UtcNow,
            };

            municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Id = IdMunicipio,
                Nome = NomeMunicipioAlterado,
                CodIBGE = CodigoIBGEMunicipioAlterado,
                UfId = IdUf
            };

            municipioDtoUpdateResult = new MunicipioDtoUpdateResult
            {
                Id = IdMunicipio,
                Nome = NomeMunicipioAlterado,
                CodIBGE = CodigoIBGEMunicipioAlterado,
                UfId = IdUf,
                UpdateAt = DateTime.UtcNow,
            };

            viaCepDto = new ViaCepDto()
            {
                Cep = "12345678",
                Logradouro = "Rua Teste",
                Complemento = "Apto 101",
                Bairro = "Bairro Teste",
                Localidade = NomeMunicipio,
                Uf = "SP",
                Ibge = CodigoIBGEMunicipio.ToString(),
                Gia = "1234",
                Ddd = "11",
                Siafi = "1234"
            };
        }
    }
}
