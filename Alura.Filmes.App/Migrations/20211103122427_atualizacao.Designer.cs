﻿// <auto-generated />
using Alura.Filmes.App.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Alura.Filmes.App.Migrations
{
    [DbContext(typeof(AluraFilmesContexto))]
    [Migration("20211103122427_atualizacao")]
    partial class atualizacao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alura.Filmes.App.Negocio.Ator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("actor_id");

                    b.Property<string>("PrimeiroNome")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("UltimoNome")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("varchar(45)");

                    b.Property<DateTime>("last_update")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("PrimeiroNome", "UltimoNome");

                    b.HasIndex("UltimoNome")
                        .HasName("idx_actor_last_name");

                    b.ToTable("actor");
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.Categoria", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnName("category_id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime>("last_update")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("film_id");

                    b.Property<string>("AnoLancamento")
                        .HasColumnName("release_year")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Classificacao")
                        .HasColumnName("rating")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Descricao")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<short>("Duracao")
                        .HasColumnName("length");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(255)");

                    b.Property<byte>("language_id");

                    b.Property<DateTime>("last_update")
                        .HasColumnType("datetime");

                    b.Property<byte?>("original_language_id");

                    b.HasKey("Id");

                    b.HasIndex("language_id");

                    b.HasIndex("original_language_id");

                    b.ToTable("film");
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.FilmeAtor", b =>
                {
                    b.Property<int>("film_id");

                    b.Property<int>("actor_id");

                    b.Property<DateTime>("last_update")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("film_id", "actor_id");

                    b.HasIndex("actor_id");

                    b.ToTable("film_actor");
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.FilmeCategoria", b =>
                {
                    b.Property<int>("film_id");

                    b.Property<byte>("category_id");

                    b.HasKey("film_id", "category_id");

                    b.HasIndex("category_id");

                    b.ToTable("film_category");
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.Idioma", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnName("language_id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("char(20)");

                    b.Property<DateTime>("last_update")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.ToTable("language");
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.Filme", b =>
                {
                    b.HasOne("Alura.Filmes.App.Negocio.Idioma", "IdiomaFalado")
                        .WithMany("FilmesFalados")
                        .HasForeignKey("language_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Alura.Filmes.App.Negocio.Idioma", "IdiomaOriginal")
                        .WithMany("FilmesOriginais")
                        .HasForeignKey("original_language_id");
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.FilmeAtor", b =>
                {
                    b.HasOne("Alura.Filmes.App.Negocio.Ator", "Ator")
                        .WithMany("Filmografia")
                        .HasForeignKey("actor_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Alura.Filmes.App.Negocio.Filme", "Filme")
                        .WithMany("Atores")
                        .HasForeignKey("film_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alura.Filmes.App.Negocio.FilmeCategoria", b =>
                {
                    b.HasOne("Alura.Filmes.App.Negocio.Categoria", "Categoria")
                        .WithMany("Filmes")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Alura.Filmes.App.Negocio.Filme", "Filme")
                        .WithMany("Categorias")
                        .HasForeignKey("film_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}