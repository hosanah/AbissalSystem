using AbissalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbissalSystem.Data.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Tabela
            builder.ToTable("Client");

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
            builder.Property(x => x.Nickname)
                .IsRequired()
                .HasColumnName("Nickname")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            // Propriedades
            builder.Property(x => x.Birthday)
                .IsRequired()
                .HasColumnName("Birthday")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

             // Propriedades
            builder.Property(x => x.CallPhone)
                .IsRequired()
                .HasColumnName("Callphone")
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