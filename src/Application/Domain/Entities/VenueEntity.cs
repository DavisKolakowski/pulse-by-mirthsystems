using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NetTopologySuite.Geometries;

using NodaTime;

namespace Application.Domain.Entities;

[Table("venues")]
public class VenueEntity : EntityBase
{
    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [Column("name")]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [MaxLength(500)]
    public string? Description { get; set; }

    [Column("phone_number")]
    [Phone]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("website")]
    [Url]
    [MaxLength(200)]
    public string? Website { get; set; }

    [Column("email")]
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("profile_image")]
    [Url]
    [MaxLength(500)]
    public string? ProfileImage { get; set; }

    [Column("street_address")]
    [Required]
    [MaxLength(200)]
    public string StreetAddress { get; set; } = null!;

    [Column("secondary_address")]
    [MaxLength(100)]
    public string? SecondaryAddress { get; set; }

    [Column("locality")]
    [Required]
    [MaxLength(100)]
    public string Locality { get; set; } = null!;

    [Column("region")]
    [Required]
    [MaxLength(50)]
    public string Region { get; set; } = null!;

    [Column("postal_code")]
    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; } = null!;

    [Column("country")]
    [Required]
    [MaxLength(50)]
    public string Country { get; set; } = null!;

    [Column("location", TypeName = "geography (point)")]
    [Required]
    public Point Location { get; set; } = null!;

    [Column("is_active")]
    [DefaultValue(true)]
    public bool IsActive { get; set; }

    public VenueCategoryEntity Category { get; set; } = null!;
    public List<BusinessHoursEntity> BusinessHours { get; set; } = new List<BusinessHoursEntity>();
    public List<SpecialEntity> Specials { get; set; } = new List<SpecialEntity>();
    public List<SpecialMenuEntity> SpecialMenus { get; set; } = new List<SpecialMenuEntity>();
    public List<VenueUserRoleEntity> VenueUsers { get; set; } = new List<VenueUserRoleEntity>();
    public List<VenueInvitationEntity> VenueInvitations { get; set; } = new List<VenueInvitationEntity>();
}
