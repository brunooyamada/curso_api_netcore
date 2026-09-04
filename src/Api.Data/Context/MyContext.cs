using Data.Mapping;
using Data.Seeds;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            modelBuilder.Entity<UfEntity>(new UfMap().Configure);
            modelBuilder.Entity<MunicipioEntity>(new MunicipioMap().Configure);
            modelBuilder.Entity<CepEntity>(new CepMap().Configure);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    Name = "Administrador",
                    Email = "mfrinfo@mail.com",
                    CreateAt = new DateTime(2025, 1, 1, 00, 0, 0, DateTimeKind.Utc),
                    UpdateAt = new DateTime(2025, 1, 1, 00, 0, 0, DateTimeKind.Utc),
                }
            );

            UfSeeds.Ufs(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyContext).Assembly);

        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
