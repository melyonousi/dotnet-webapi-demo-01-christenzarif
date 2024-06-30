using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi_demo_01_christenzarif.Models
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .Property(e => e.UserName)
            .HasComputedColumnSql("SUBSTRING(Email, 1, CHARINDEX('@', Email) - 1)");

            modelBuilder.Entity<Employee>().Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Employee>().Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
