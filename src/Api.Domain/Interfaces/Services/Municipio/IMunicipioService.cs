using Domain.Dtos.Municipio;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Municipio
{
    public interface IMunicipioService
    {
        Task<MunicipioDto> Get(long id);
        Task<MunicipioDtoCompleto> GetCompleteById(long id);
        Task<MunicipioDtoCompleto> GetCompletebyIBGE(int codIBGE);
        Task<IEnumerable<MunicipioDto>> GetAll();
        Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipio);
        Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipio);
        Task<bool> Delete(long id);
    }
}
