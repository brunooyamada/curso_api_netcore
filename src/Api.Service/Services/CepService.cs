using AutoMapper;
using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Domain.Dtos.ViaCep;
using Domain.Entities;
using Domain.Interfaces.Services.Cep;
using Domain.Interfaces.Services.Municipio;
using Domain.Interfaces.Services.Uf;
using Domain.Models;
using Domain.Repository;
using Newtonsoft.Json;
using RestSharp;

namespace Service.Services
{
    public class CepService : ICepService
    {
        private ICepRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUfService _ufService;
        private readonly IMunicipioService _municipioService;

        public CepService(ICepRepository repository, IMapper mapper, IUfService ufService, IMunicipioService municipioService)
        {
            _repository = repository;
            _mapper = mapper;
            _ufService = ufService;
            _municipioService = municipioService;
        }

        public async Task<CepDto> Get(long id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<CepDto>(entity);
        }

        public async Task<CepDto> Get(string cep)
        {
            var entity = await _repository.SelectAsync(cep);
            return _mapper.Map<CepDto>(entity);
        }

        public async Task<CepDtoCreateResult> Post(CepDtoCreate cep)
        {
            var model = _mapper.Map<CepModel>(cep);
            var entity = _mapper.Map<CepEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<CepDtoCreateResult>(result);
        }

        public async Task<CepDtoUpdateResult> Put(CepDtoUpdate cep)
        {
            var model = _mapper.Map<CepModel>(cep);
            var entity = _mapper.Map<CepEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<CepDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(long id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CepDto> GetByApi(string cep)
        {
            var existingEntity = await _repository.SelectAsync(cep);
            if (existingEntity != null)
                return _mapper.Map<CepDto>(existingEntity);

            var viaCepDto = await GetFromViaCep(cep) ?? throw new ArgumentException("Erro ao consultar o cep");
            var uf = await _ufService.GetPorSigla(viaCepDto.Uf)
                     ?? throw new ArgumentException("UF não encontrada");

            var municipio = await GetOrCreateMunicipio(viaCepDto, uf.Id);

            var newEntity = new CepEntity
            {
                Cep = viaCepDto.Cep,
                Logradouro = viaCepDto.Logradouro,
                Numero = "S/N",
                MunicipioId = municipio.Id
            };

            var inserted = await _repository.InsertAsync(newEntity);
            return _mapper.Map<CepDto>(inserted);
        }

        private async Task<ViaCepDto?> GetFromViaCep(string cep)
        {
            var client = new RestClient("https://viacep.com.br");
            var request = new RestRequest($"/ws/{cep}/json/", Method.Get);
            var response = await client.ExecuteAsync(request);

            return string.IsNullOrWhiteSpace(response.Content)
                ? null
                : JsonConvert.DeserializeObject<ViaCepDto>(response.Content);
        }

        private async Task<MunicipioDtoCompleto> GetOrCreateMunicipio(ViaCepDto viaCepDto, long ufId)
        {
            var municipio = await _municipioService.GetCompleteByIBGE(int.Parse(viaCepDto.Ibge));
            if (municipio != null) return municipio;

            var newMunicipio = await _municipioService.Post(new MunicipioDtoCreate
            {
                Nome = viaCepDto.Localidade,
                CodIBGE = int.Parse(viaCepDto.Ibge),
                UfId = ufId
            });

            var municipioCompleto = await _municipioService.GetCompleteById(newMunicipio.Id);

            return _mapper.Map<MunicipioDtoCompleto>(municipioCompleto);
        }
    }
}
