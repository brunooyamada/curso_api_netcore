using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Api.Integration.Test.Cep
{
    public class QuandoRequisitarCep : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Executar_Crud_Cep()
        {
            await AdicionarToken();

            // Post
            #region "Metodo Post de Municipio"
            var municipioDto = new MunicipioDtoCreate()
            {
                Nome = "São Paulo",
                CodIBGE = 3550308,
                UfId = 25,
            };

            var response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPostMunicipio = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("São Paulo", registroPostMunicipio.Nome);
            Assert.Equal(3550308, registroPostMunicipio.CodIBGE);
            Assert.True(registroPostMunicipio.Id != null);
            #endregion

            var cepDto = new CepDtoCreate()
            {
                Cep = Faker.Address.UkPostCode(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 20000).ToString(),
                MunicipioId = registroPostMunicipio.Id,
            };

            // Post
            #region Post
            response = await PostJsonAsync(cepDto, $"{hostApi}ceps", client);
            postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<CepDtoCreateResult>(postResult);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(cepDto.Cep, registroPost.Cep);
            Assert.Equal(cepDto.Logradouro, registroPost.Logradouro);
            Assert.Equal(cepDto.Numero, registroPost.Numero);
            Assert.Equal(cepDto.MunicipioId, registroPost.MunicipioId);
            Assert.True(registroPost.Id != null);
            #endregion

            // Put
            #region Put
            var updateCepDto = new CepDtoUpdate()
            {
                Id = registroPost.Id,
                Cep = Faker.Address.UkPostCode(),
                Logradouro = Faker.Address.StreetName(),
                Numero = Faker.RandomNumber.Next(1, 20000).ToString(),
                MunicipioId = registroPostMunicipio.Id,
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateCepDto), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}ceps", stringContent);
            var jsonResult = await response.Content.ReadAsStringAsync();

            var registroAtualizado = JsonConvert.DeserializeObject<CepDtoUpdateResult>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(updateCepDto.Cep, registroAtualizado.Cep);
            Assert.Equal(updateCepDto.Logradouro, registroAtualizado.Logradouro);
            Assert.Equal(updateCepDto.Numero, registroAtualizado.Numero);
            Assert.Equal(updateCepDto.MunicipioId, registroAtualizado.MunicipioId);
            #endregion

            // Get Id
            #region Get por Id
            response = await client.GetAsync($"{hostApi}ceps/{registroPost.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResultGet = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResultGet);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(updateCepDto.Cep, registroSelecionado.Cep);
            Assert.Equal(updateCepDto.Logradouro, registroSelecionado.Logradouro);
            Assert.Equal(updateCepDto.Numero, registroSelecionado.Numero);
            Assert.Equal(updateCepDto.MunicipioId, registroSelecionado.MunicipioId);
            #endregion

            // Get cep
            #region Get por cep
            response = await client.GetAsync($"{hostApi}ceps/byCep/{updateCepDto.Cep}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(updateCepDto.Cep, registroSelecionado.Cep);
            Assert.Equal(updateCepDto.Logradouro, registroSelecionado.Logradouro);
            Assert.Equal(updateCepDto.Numero, registroSelecionado.Numero);
            Assert.Equal(updateCepDto.MunicipioId, registroSelecionado.MunicipioId);
            #endregion

            // Get por Api
            #region Get por Api
            response = await PostJsonAsync(cepDto, $"{hostApi}ceps/Buscar/{registroSelecionado.Cep}", client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoApi = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(registroSelecionadoApi);
            Assert.Equal(updateCepDto.Cep, registroSelecionadoApi.Cep);
            #endregion

            // DELETE
            #region Delete
            response = await client.DeleteAsync($"{hostApi}ceps/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            #endregion

            // Get depois do Delete
            #region Get depois do Delete
            response = await client.GetAsync($"{hostApi}ceps/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            #endregion

        }
    }
}
