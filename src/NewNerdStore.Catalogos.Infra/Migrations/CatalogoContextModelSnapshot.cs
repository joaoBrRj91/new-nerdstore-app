﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewNerdStore.Catalogos.Infra.Contexts;

#nullable disable

namespace NewNerdStore.Catalogos.Infra.Migrations
{
    [DbContext(typeof(CatalogoContext))]
    partial class CatalogoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NewNerdStore.Catalogos.Domain.Entities.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Categorias", (string)null);
                });

            modelBuilder.Entity("NewNerdStore.Catalogos.Domain.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("QuantidadeEstoque")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("NewNerdStore.Catalogos.Domain.Entities.Produto", b =>
                {
                    b.HasOne("NewNerdStore.Catalogos.Domain.Entities.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("NewNerdStore.Catalogos.Domain.ValueObjects.Dimensoes", "Dimensoes", b1 =>
                        {
                            b1.Property<Guid>("ProdutoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Altura")
                                .HasColumnType("int")
                                .HasColumnName("Altura");

                            b1.Property<int>("Largura")
                                .HasColumnType("int")
                                .HasColumnName("Largura");

                            b1.Property<int>("Profundidade")
                                .HasColumnType("int")
                                .HasColumnName("Profundidade");

                            b1.HasKey("ProdutoId");

                            b1.ToTable("Produtos");

                            b1.WithOwner()
                                .HasForeignKey("ProdutoId");
                        });

                    b.Navigation("Categoria");

                    b.Navigation("Dimensoes");
                });

            modelBuilder.Entity("NewNerdStore.Catalogos.Domain.Entities.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
