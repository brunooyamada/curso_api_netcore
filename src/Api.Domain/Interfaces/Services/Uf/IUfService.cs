using Domain.Dtos.Uf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Uf
{
    public interface IUfService
    {
        Task<UfDto> Get(long id);
        Task<IEnumerable<UfDto>> GetAll();
    }
}
