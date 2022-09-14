using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbissalSystem.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Tabela
            builder.ToTable("Cliente");

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
            builder.Property(x => x.Apelido)
                .IsRequired()
                .HasColumnName("Apelido")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            // Propriedades
            builder.Property(x => x.DataAniversario)
                .IsRequired()
                .HasColumnName("DataAniversario")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

             // Propriedades
            builder.Property(x => x.NumeroCelular)
                .IsRequired()
                .HasColumnName("NumeroCelular")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

             // Propriedades
            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
          
        }
    }
}