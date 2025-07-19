using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using NodaTime;

namespace Application.Infrastructure.Data.ValueGenerators;

public class CurrentInstantValueGenerator : ValueGenerator<Instant>
{
    public override bool GeneratesTemporaryValues => false;

    public override Instant Next(EntityEntry entry) => SystemClock.Instance.GetCurrentInstant();
}
