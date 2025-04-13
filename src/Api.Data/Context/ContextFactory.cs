using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // Usado para criar as migrações
            var connectionString = "Server=localhost;Port=5432;Database=dbApi;Uid=postgres;Pwd=masterkey";
            //var connectionString = "Password=masterkey;Persist Security Info=True;User ID=sa;Initial Catalog=dbApi;Data Source=bruno\\sqlexpress;TrustServerCertificate=True";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseNpgsql(connectionString);
            //optionsBuilder.UseSqlServer(connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
