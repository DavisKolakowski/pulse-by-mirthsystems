using NodaTime;

namespace Application.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; }

    public Instant CreatedAt { get; set; }

    public Instant? UpdatedAt { get; set; }
}
