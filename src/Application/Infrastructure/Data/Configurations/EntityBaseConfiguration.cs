using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Application.Domain.Entities;
using Application.Infrastructure.Data.ValueGenerators;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NodaTime;

using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;

namespace Application.Infrastructure.Data.Configurations;

public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasValueGenerator<GuidV7ValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasValueGenerator<CurrentInstantValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasValueGenerator<CurrentInstantValueGenerator>()
            .ValueGeneratedOnUpdate();
    }
}
