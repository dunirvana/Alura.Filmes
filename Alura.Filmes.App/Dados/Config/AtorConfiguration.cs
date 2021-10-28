﻿using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados.Config
{
    public class AtorConfiguration : IEntityTypeConfiguration<Ator>
    {
        public void Configure(EntityTypeBuilder<Ator> builder)
        {
            builder
                 .ToTable("actor");

            builder
                 .Property(a => a.Id)
                 .HasColumnName("actor_id");

            builder
                 .Property(a => a.PrimeiroNome)
                 .HasColumnName("first_name")
                 .HasColumnType("varchar(45)")
                 .IsRequired();

            builder
                 .Property(a => a.UltimoNome)
                 .HasColumnType("varchar(45)")
                 .IsRequired();

            builder
                 .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

        }
    }
}
