using Domain.Dtos.Municipio;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Api.Integration.Test.Municipio
{
    public class QuandoRequisitarMunicipio : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Municipio()
        {
            await AdicionarToken();

            var municipioDto = new MunicipioDtoCreate ()
            {
                Nome = "São Paulo",
                CodIBGE = 3550308,
                UfId = 25,
            };

            // Post
            #region Metodo Post
            var response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("São Paulo", registroPost.Nome);
            Assert.Equal(3550308, registroPost.CodIBGE);
            Assert.True(registroPost.Id != null);
            #endregion

            // Get All
            #region Metodo GetAll
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.Id == registroPost.Id).Count() == 1);
            #endregion

            // Put
            #region Metodo Put
            var updateMunicipioDto = new MunicipioDtoUpdate()
            {
                Id = registroPost.Id,
                Nome = "Limeira",
                CodIBGE = 3526902,
                UfId = 25,
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateMunicipioDto), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Limeira", registroAtualizado.Nome);
            Assert.Equal(3526902, registroAtualizado.CodIBGE);
            #endregion

            // Get Id
            #region Metodo GetId
            response = await client.GetAsync($"{hostApi}municipios/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionado.CodIBGE, registroAtualizado.CodIBGE);
            #endregion

            // Get Complete/Id
            #region Metodo GetCompleteId
            response = await client.GetAsync($"{hostApi}municipios/complete/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoComplete = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoComplete);
            Assert.Equal(registroSelecionadoComplete.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoComplete.CodIBGE, registroAtualizado.CodIBGE);
            Assert.NotNull(registroSelecionadoComplete.Uf);
            Assert.Equal("São Paulo", registroSelecionadoComplete.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoComplete.Uf.Sigla);
            #endregion

            // Get byIBGE/IBGE
            #region Metodo GetCompleteByIBGE
            response = await client.GetAsync($"{hostApi}municipios/ByIBGE/{registroAtualizado.CodIBGE}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoIBGEComplete = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoIBGEComplete);
            Assert.Equal(registroSelecionadoIBGEComplete.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoIBGEComplete.CodIBGE, registroAtualizado.CodIBGE);
            Assert.NotNull(registroSelecionadoIBGEComplete.Uf);
            Assert.Equal("São Paulo", registroSelecionadoIBGEComplete.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoIBGEComplete.Uf.Sigla);
            #endregion

            // DELETE
            #region Metodo Delete
            response = await client.DeleteAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Get por Id depois do DELETE
            response = await client.GetAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            #endregion
        }
    }
}
