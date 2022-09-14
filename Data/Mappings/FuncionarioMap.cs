using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbissalSystem.Data.Mappings
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            // Tabela
            builder.ToTable("Funcionario");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasColumnName("NomeCompleto")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            
            // Propriedades
            builder.Property(x => x.DataAniversario)
                .IsRequired()
                .HasColumnName("DataAniversario")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            /*builder.Property(x => x.)
                .IsRequired()
                .HasColumnName("Documentid")
                .HasColumnType("INT");*/

            /*builder.Property(x => x.)
                .IsRequired()
                .HasColumnName("DocumentNumber")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);*/
            
            // Propriedades
            builder.Property(x => x.Salario)
                .IsRequired()
                .HasColumnName("Salario")
                .HasColumnType("FLOAT")
                .HasMaxLength(60);
            
          
        }
    }
}