﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class CepMap : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(EntityTypeBuilder<CepEntity> builder)
        {
            builder.ToTable("Cep");

            builder.HasKey(x => x.Id);

            builder.HasIndex(u => u.Cep);

            builder.HasOne(c => c.Municipio)
                .WithMany(m => m.Ceps);
        }
    }
}
