using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi_demo_01_christenzarif.Models
{
    public class DataContext: DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options): base(options){ }

        //public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
