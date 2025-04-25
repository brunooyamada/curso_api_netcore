using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    public static class UfSeeds
    {
        public static void Ufs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UfEntity>().HasData(
                new UfEntity
                {
                    Id = 1,
                    Sigla = "AC",
                    Nome = "Acre",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 2,
                    Sigla = "AL",
                    Nome = "Alagoas",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 3,
                    Sigla = "AP",
                    Nome = "Amapá",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 4,
                    Sigla = "AM",
                    Nome = "Amazonas",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 5,
                    Sigla = "BA",
                    Nome = "Bahia",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 6,
                    Sigla = "CE",
                    Nome = "Ceará",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 7,
                    Sigla = "DF",
                    Nome = "Distrito Federal",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 8,
                    Sigla = "ES",
                    Nome = "Espírito Santo",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 9,
                    Sigla = "GO",
                    Nome = "Goiás",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 10,
                    Sigla = "MA",
                    Nome = "Maranhão",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 11,
                    Sigla = "MT",
                    Nome = "Mato Grosso",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 12,
                    Sigla = "MS",
                    Nome = "Mato Grosso do Sul",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 13,
                    Sigla = "MG",
                    Nome = "Minas Gerais",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 14,
                    Sigla = "PA",
                    Nome = "Pará",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 15,
                    Sigla = "PB",
                    Nome = "Paraíba",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 16,
                    Sigla = "PR",
                    Nome = "Paraná",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 17,
                    Sigla = "PE",
                    Nome = "Pernambuco",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 18,
                    Sigla = "PI",
                    Nome = "Piauí",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 19,
                    Sigla = "RJ",
                    Nome = "Rio de Janeiro",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 20,
                    Sigla = "RN",
                    Nome = "Rio Grande do Norte",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 21,
                    Sigla = "RS",
                    Nome = "Rio Grande do Sul",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 22,
                    Sigla = "RO",
                    Nome = "Rondônia",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 23,
                    Sigla = "RR",
                    Nome = "Roraima",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 24,
                    Sigla = "SC",
                    Nome = "Santa Catarina",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 25,
                    Sigla = "SP",
                    Nome = "São Paulo",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 26,
                    Sigla = "SE",
                    Nome = "Sergipe",
                    CreateAt = DateTime.UtcNow,
                },
                new UfEntity
                {
                    Id = 27,
                    Sigla = "TO",
                    Nome = "Tocantins",
                    CreateAt = DateTime.UtcNow,
                }
            );
        }
    }
}
