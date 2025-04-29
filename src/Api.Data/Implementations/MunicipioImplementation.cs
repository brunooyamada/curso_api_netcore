using Data.Context;
using Data.Repository;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
    public class MunicipioImplementation : BaseRepository<MunicipioEntity>, IMunicipioRepository
    {
        private DbSet<MunicipioEntity> _dataSet;

        public MunicipioImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<MunicipioEntity>();
        }

        public async Task<MunicipioEntity> GetCompleteByIBGE(int codIBGE)
        {
            return await _dataSet.Include(m => m.Uf)
                .FirstOrDefaultAsync(m => m.CodIBGE.Equals(codIBGE));
        }

        public async Task<MunicipioEntity> GetCompleteById(long id)
        {
            return await _dataSet.Include(m => m.Uf)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
        }

    }
}
