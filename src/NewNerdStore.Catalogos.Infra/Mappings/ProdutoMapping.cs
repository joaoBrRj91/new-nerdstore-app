using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewNerdStore.Catalogos.Domain.Entities;

namespace NewNerdStore.Catalogos.Infra.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");


            builder.OwnsOne(c => c.Dimensoes,
                vo =>
                {
                    vo.Property(p => p.Altura).HasColumnName("Altura").HasColumnType("int");
                    vo.Property(p => p.Largura).HasColumnName("Largura").HasColumnType("int");
                    vo.Property(p => p.Profundidade).HasColumnName("Profundidade").HasColumnType("int");

                });

            builder.ToTable("Produtos");
        }
    }
}
