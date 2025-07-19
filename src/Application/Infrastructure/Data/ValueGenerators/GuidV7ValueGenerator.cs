using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using NodaTime;

namespace Application.Infrastructure.Data.ValueGenerators;


public class GuidV7ValueGenerator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry) => Guid.CreateVersion7(SystemClock.Instance.GetCurrentInstant().ToDateTimeOffset());
}
