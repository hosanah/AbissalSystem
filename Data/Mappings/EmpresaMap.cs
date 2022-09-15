using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbissalSystem.Data.Mappings
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            // Tabela
            builder.ToTable("Empresa");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.NomeFantasia)
                .IsRequired()
                .HasColumnName("NomeFantasia")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.RazaoSocial)
                .IsRequired()
                .HasColumnName("RazaoSocial")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            
            builder.Property(x => x.CNPJ)
                .IsRequired()
                .HasColumnName("CNPJ")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
                                     
        }
    }
}