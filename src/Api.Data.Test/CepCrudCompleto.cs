﻿using Data.Context;
using Data.Implementations;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
    public class CepCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;
        public CepCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de CEP")]
        [Trait("CRUD", "CepEntity")]
        public async Task E_Possivel_Realizar_CRUD_Cep()
        {
            using (var _context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repositorioMunicipio = new MunicipioImplementation(_context);
                MunicipioEntity _entityMunicipio = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = 25,
                };

                var _registroCriado = await _repositorioMunicipio.InsertAsync(_entityMunicipio);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entityMunicipio.Nome, _registroCriado.Nome);
                Assert.Equal(_entityMunicipio.CodIBGE, _registroCriado.CodIBGE);
                Assert.Equal(_entityMunicipio.UfId, _registroCriado.UfId);
                Assert.False(_registroCriado.Id == 0);

                CepImplementation _repositorio = new CepImplementation(_context);
                CepEntity _entityCep = new CepEntity
                {
                    Cep = Faker.Address.ZipCode(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = "0 a 2000",
                    MunicipioId = _registroCriado.Id,
                };

                var _registroCriadoCep = await _repositorio.InsertAsync(_entityCep);
                Assert.NotNull(_registroCriadoCep);
                Assert.Equal(_entityCep.Cep, _registroCriadoCep.Cep);
                Assert.Equal(_entityCep.Logradouro, _registroCriadoCep.Logradouro);
                Assert.Equal(_entityCep.Numero, _registroCriadoCep.Numero);
                Assert.Equal(_entityCep.MunicipioId, _registroCriadoCep.MunicipioId);
                Assert.False(_registroCriadoCep.Id == 0);

                // Atualizacao
                _entityCep.Logradouro = Faker.Address.StreetName();
                _entityCep.Id = _registroCriadoCep.Id;
                var _registroAtualizado = await _repositorio.UpdateAsync(_entityCep);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entityCep.Cep, _registroAtualizado.Cep);
                Assert.Equal(_entityCep.Logradouro, _registroAtualizado.Logradouro);
                Assert.Equal(_entityCep.MunicipioId, _registroAtualizado.MunicipioId);
                Assert.Equal(_registroCriadoCep.Id, _registroAtualizado.Id);
                Assert.True(_registroCriadoCep.Id == _entityCep.Id);

                // Registro Existe
                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                // Select por id
                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Cep, _registroSelecionado.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelecionado.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelecionado.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelecionado.MunicipioId);

                _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Cep);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Cep, _registroSelecionado.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelecionado.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelecionado.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelecionado.MunicipioId);
                Assert.NotNull(_registroSelecionado.Municipio);
                Assert.Equal(_entityMunicipio.Nome, _registroSelecionado.Municipio.Nome);
                Assert.NotNull(_registroSelecionado.Municipio.Uf);
                Assert.Equal("SP", _registroSelecionado.Municipio.Uf.Sigla);

                // Select *
                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                // Delete
                var removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                Assert.True(removeu);

                _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 0);
            }
        }

    }
}
