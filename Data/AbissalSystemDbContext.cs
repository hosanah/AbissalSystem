using AbissalSystem.Data.Mappings;
using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AbissalSystem.Data
{
    
    public class AbissalSystemDbContext : DbContext
    {
         public AbissalSystemDbContext(DbContextOptions<AbissalSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }        
        public DbSet<Empresa> Empresas { get; set; }        
        public DbSet<Funcionario> Funcionarios { get; set; } 
        public DbSet<Produto> Produtos { get; set; }        
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Regra> Regras { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }

}
