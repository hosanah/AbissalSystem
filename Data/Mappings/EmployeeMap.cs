using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbissalSystem.Data.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Tabela
            builder.ToTable("Employee");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasColumnName("Fullname")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            
            // Propriedades
            builder.Property(x => x.Birthday)
                .IsRequired()
                .HasColumnName("Birthday")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            builder.Property(x => x.DocumentId)
                .IsRequired()
                .HasColumnName("Documentid")
                .HasColumnType("INT");

            builder.Property(x => x.DocumentNumber)
                .IsRequired()
                .HasColumnName("DocumentNumber")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            
            // Propriedades
            builder.Property(x => x.Salary)
                .IsRequired()
                .HasColumnName("Salary")
                .HasColumnType("FLOAT")
                .HasMaxLength(60);
            
          
        }
    }
}