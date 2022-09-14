using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbissalSystem.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Tabela
            builder.ToTable("Produto");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnName("Descricao")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Preco)
                .IsRequired()
                .HasColumnName("Preco")
                .HasColumnType("FLOAT")
                .HasMaxLength(20);

            // Relacionamentos
            builder
                .HasOne(x => x.Empresa)
                .WithMany(x => x.Produtos)
                .HasConstraintName("FK_Produtos_User")
                .OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}