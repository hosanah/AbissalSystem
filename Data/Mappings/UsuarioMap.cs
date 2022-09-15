using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbissalSystem.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Tabela
            builder.ToTable("Usuario");
           
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
            
            // Propriedades
            builder.Property(x => x.NomeUsuario)
                .IsRequired()
                .HasColumnName("NomeUsuario")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(40);

            builder.Property(x => x.SenhaHash).IsRequired()
                .HasColumnName("SenhaHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

             builder
                .HasMany(x => x.Regras)
                .WithMany(x => x.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    role => role
                        .HasOne<Regra>()
                        .WithMany()
                        .HasForeignKey("RegraId")
                        .HasConstraintName("FK_UsuarioRegra_RegraId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UsuarioRegra_UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade));

        }
    }
}