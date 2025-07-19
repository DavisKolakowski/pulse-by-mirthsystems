using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using NodaTime;

namespace Application.Infrastructure.Data.ValueGenerators;


public class GuidV7ValueGenerator : ValueGenerator<Guid>
{
    private readonly IClock _clock;

    public GuidV7ValueGenerator(IClock clock)
    {
        this._clock = clock;
    }

    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry) => Guid.CreateVersion7(this._clock.GetCurrentInstant().ToDateTimeOffset());
}
