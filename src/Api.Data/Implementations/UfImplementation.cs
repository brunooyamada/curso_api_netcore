using Data.Context;
using Data.Repository;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
    public class UfImplementation : BaseRepository<UfEntity>, IUfRepository
    {
        private DbSet<UfEntity> _dataSet;

        public UfImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<UfEntity>();

        }
    }
}
