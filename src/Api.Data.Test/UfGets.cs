﻿using Data.Context;
using Data.Implementations;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
    public class UfGets : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UfGets(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "Gets de UF")]
        [Trait("GET", "UfEntity")]
        public async Task E_Possivel_Realizar_Gets_UF()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repositorio = new UfImplementation(context);
                UfEntity _entity = new UfEntity
                {
                    Id = 25,
                    Sigla = "SP",
                    Nome = "São Paulo",
                };

                var _registroExiste = await _repositorio.ExistAsync(_entity.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repositorio.SelectAsync(_entity.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_entity.Sigla, _registroSelecionado.Sigla);
                Assert.Equal(_entity.Nome, _registroSelecionado.Nome);
                Assert.Equal(_entity.Id, _registroSelecionado.Id);

                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 27);
            }
        }

        [Fact(DisplayName = "GetPorSigla de UF")]
        public async Task Get_Por_Sigla()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repositorio = new UfImplementation(context);
                var _registroSelecionado = await _repositorio.GetPorSigla("SP");
                Assert.NotNull(_registroSelecionado);
                Assert.Equal("SP", _registroSelecionado.Sigla);
                Assert.Equal("São Paulo", _registroSelecionado.Nome);
            }
        }
    }
}
