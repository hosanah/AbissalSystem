using AbissalSystem.Data.Mappings;
using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AbissalSystem.Data
{
    
    public class AbissalSystemDbContext : DbContext
    {

        public DbSet<Client> Clients { get; set; }
        
        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Enterprise> Enterprises { get; set; }
        
        public DbSet<Product> Products { get; set; }
        
        public DbSet<User> Users { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=192.168.150.7,1433;Database=AbissalSystem;User ID=sa;Password=Dbamaster.");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new EnterpriseMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }

}
