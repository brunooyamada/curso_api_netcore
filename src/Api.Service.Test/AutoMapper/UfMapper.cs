using Domain.Dtos.Uf;
using Domain.Entities;
using Domain.Models;

namespace Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTestService
    {
        [Fact(DisplayName = "É possível Mappear os Modelos de Uf")]
        public void E_Possivel_Mapear_os_Modelos_Uf()
        {
            var model = new UfModel
            {
                Id = 1,
                Nome = Faker.Address.UsState(),
                Sigla = Faker.Address.UsState().Substring(1, 3),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
            };

            var listaEntity = new List<UfEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UfEntity
                {
                    Id = i,
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                };

                listaEntity.Add(item);
            }

            // Model => Entity
            var entity = Mapper.Map<UfEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.Sigla, model.Sigla);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity para Dto
            var ufDto = Mapper.Map<UfDto>(entity);
            Assert.Equal(ufDto.Id, entity.Id);
            Assert.Equal(ufDto.Nome, entity.Nome);
            Assert.Equal(ufDto.Sigla, entity.Sigla);

            var listaDto = Mapper.Map<List<UfDto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++)
            {
                Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listaDto[i].Sigla, listaEntity[i].Sigla);
            }

            // Dto para Model
            var ufModel = Mapper.Map<UfDto>(model);
            Assert.Equal(ufModel.Id, model.Id);
            Assert.Equal(ufModel.Nome, model.Nome);
            Assert.Equal(ufModel.Sigla, model.Sigla);

        }
    }
}
