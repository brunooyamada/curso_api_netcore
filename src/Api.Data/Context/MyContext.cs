using Data.Mapping;
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

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    Name = "Administrador",
                    Email = "mfrinfo@mail.com",
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                }
            );
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
