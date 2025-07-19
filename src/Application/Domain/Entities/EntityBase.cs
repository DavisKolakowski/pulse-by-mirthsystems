using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Application.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; }

    public Instant CreatedAt { get; private set; }

    public Instant? UpdatedAt { get; private set; }
}
