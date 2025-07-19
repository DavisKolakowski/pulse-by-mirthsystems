using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using NodaTime;

namespace Application.Infrastructure.Data.ValueGenerators;

public class CurrentInstantValueGenerator : ValueGenerator<Instant>
{
    private readonly IClock _clock;

    public CurrentInstantValueGenerator(IClock clock)
    {
        this._clock = clock;
    }

    public override bool GeneratesTemporaryValues => false;

    public override Instant Next(EntityEntry entry) => this._clock.GetCurrentInstant();
}
