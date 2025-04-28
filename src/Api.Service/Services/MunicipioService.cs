using AutoMapper;
using Domain.Dtos.Municipio;
using Domain.Entities;
using Domain.Interfaces.Services.Municipio;
using Domain.Models;
using Domain.Repository;

namespace Service.Services
{
    public class MunicipioService : IMunicipioService
    {
        IMunicipioRepository _repository;
        private readonly IMapper _mapper;

        public MunicipioService(IMunicipioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MunicipioDto> Get(long id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<MunicipioDto>(entity);
        }

        public async Task<MunicipioDtoCompleto> GetCompleteById(long id)
        {
            var entity = await _repository.GetCompletebyId(id);
            return _mapper.Map<MunicipioDtoCompleto>(entity);
        }

        public async Task<MunicipioDtoCompleto> GetCompletebyIBGE(int codIBGE)
        {
            var entity = await _repository.GetCompletebyIBGE(codIBGE);
            return _mapper.Map<MunicipioDtoCompleto>(entity);
        }

        public async Task<IEnumerable<MunicipioDto>> GetAll()
        {
            var listEntity = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<MunicipioDto>>(listEntity);
        }

        public async Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipio)
        {
            var model = _mapper.Map<MunicipioModel>(municipio);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<MunicipioDtoCreateResult>(result);
        }

        public async Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipio)
        {
            var model = _mapper.Map<MunicipioModel>(municipio);
            var entity = _mapper.Map<MunicipioEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<MunicipioDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(long id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
