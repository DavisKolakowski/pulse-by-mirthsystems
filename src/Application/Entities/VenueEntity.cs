using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Azure.Maps.TimeZones;

using NetTopologySuite.Geometries;

namespace Application.Entities;

[Table("venues")]
public class VenueEntity : EntityBase
{
    [Column("primary_category_id")]
    public Guid PrimaryCategoryId { get; set; }

    [Column("secondary_category_id")]
    public Guid? SecondaryCategoryId { get; set; }

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

    [Column("time_zone_id")]
    [Required]
    [MaxLength(32)]
    public string TimeZoneId { get; set; } = null!; // IANA ID, e.g., "America/New_York"

    [Column("is_active")]
    [DefaultValue(true)]
    public bool IsActive { get; set; }

    public VenueCategoryEntity PrimaryCategory { get; set; } = null!;
    public VenueCategoryEntity? SecondaryCategory { get; set; }
    public List<VenueBusinessHoursEntity> BusinessHours { get; set; } = new List<VenueBusinessHoursEntity>();
    public List<SpecialMenuEntity> SpecialMenus { get; set; } = new List<SpecialMenuEntity>();
    public List<VenueUserRoleEntity> VenueUsers { get; set; } = new List<VenueUserRoleEntity>();
    public List<VenueInvitationEntity> VenueInvitations { get; set; } = new List<VenueInvitationEntity>();
}
