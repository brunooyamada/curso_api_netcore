using Domain.Dtos.Cep;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDto> Get(long id);
        Task<CepDto> Get(string cep);
        Task<CepDtoCreateResult> Post(CepDtoCreate cep);
        Task<CepDtoUpdateResult> Put(CepDtoUpdate cep);
        Task<bool> Delete(long id);
    }
}
