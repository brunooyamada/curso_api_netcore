using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IMunicipioRepository : IRepository<MunicipioEntity>
    {
        Task<MunicipioEntity> GetCompletebyId(long id);
        Task<MunicipioEntity> GetCompletebyIBGE(int codIBGE);
    }
}
