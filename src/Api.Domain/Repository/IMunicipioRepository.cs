using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IMunicipioRepository : IRepository<MunicipioEntity>
    {
        Task<MunicipioEntity> GetCompleteById(long id);
        Task<MunicipioEntity> GetCompleteByIBGE(int codIBGE);
    }
}
