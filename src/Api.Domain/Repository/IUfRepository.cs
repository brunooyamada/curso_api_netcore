using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IUfRepository : IRepository<UfEntity>
    {
        Task<UfEntity> GetPorSigla(string sigla);
    }
}
