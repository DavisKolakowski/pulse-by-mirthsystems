using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Models.BusinessHours;
using Application.Models.Categories;
using Application.Models.Location;

using NodaTime;

namespace Application.Models.Venues;


/// <summary>
/// Base venue information without specials board
/// </summary>
public class VenueItem : ItemBase
{
    /// <summary>
    /// The name of the venue
    /// </summary>
    /// <example>Example Venue</example>
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    /// <summary>
    /// The primary category for this venue
    /// </summary>
    [JsonPropertyName("primary_category")]
    [JsonPropertyOrder(2)]
    public required VenueCategoryItem PrimaryCategory { get; set; }

    /// <summary>
    /// The secondary category for this venue
    /// </summary>
    [JsonPropertyName("secondary_category")]
    [JsonPropertyOrder(3)]
    public VenueCategoryItem? SecondaryCategory { get; set; }

    /// <summary>
    /// Optional description of the venue
    /// </summary>
    /// <example>A modern event space suitable for conferences and social gatherings.</example>
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(4)]
    public string? Description { get; set; }

    /// <summary>
    /// The phone number of the venue
    /// </summary>
    /// <example>+1-555-123-4567</example>
    [Phone]
    [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    [JsonPropertyName("phone_number")]
    [JsonPropertyOrder(5)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// The website URL of the venue
    /// </summary>
    /// <example>https://www.examplevenue.com</example>
    [Url]
    [MaxLength(200, ErrorMessage = "Website URL cannot exceed 200 characters")]
    [JsonPropertyName("website")]
    [JsonPropertyOrder(6)]
    public string? Website { get; set; }

    /// <summary>
    /// The email address of the venue
    /// </summary>
    /// <example>contact@examplevenue.com</example>
    [EmailAddress]
    [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [JsonPropertyName("email")]
    [JsonPropertyOrder(7)]
    public string? Email { get; set; }

    /// <summary>
    /// Profile image URL for the venue
    /// </summary>
    /// <example>https://www.examplevenue.com/images/profile.jpg</example>
    [Url]
    [MaxLength(500, ErrorMessage = "Profile image URL cannot exceed 500 characters")]
    [JsonPropertyName("profile_image")]
    [JsonPropertyOrder(8)]
    public string? ProfileImage { get; set; }

    /// <summary>
    /// Street address of the venue
    /// </summary>
    /// <example>123 Main Street</example>
    [Required]
    [MaxLength(200, ErrorMessage = "Street address cannot exceed 200 characters")]
    [JsonPropertyName("street_address")]
    [JsonPropertyOrder(9)]
    public required string StreetAddress { get; set; }

    /// <summary>
    /// Secondary address line
    /// </summary>
    /// <example>Suite 100</example>
    [MaxLength(100, ErrorMessage = "Secondary address cannot exceed 100 characters")]
    [JsonPropertyName("secondary_address")]
    [JsonPropertyOrder(10)]
    public string? SecondaryAddress { get; set; }

    /// <summary>
    /// City or locality
    /// </summary>
    /// <example>Downtown</example>
    [Required]
    [MaxLength(100, ErrorMessage = "Locality cannot exceed 100 characters")]
    [JsonPropertyName("locality")]
    [JsonPropertyOrder(11)]
    public required string Locality { get; set; }

    /// <summary>
    /// State or region
    /// </summary>
    /// <example>California</example>
    [Required]
    [MaxLength(50, ErrorMessage = "Region cannot exceed 50 characters")]
    [JsonPropertyName("region")]
    [JsonPropertyOrder(12)]
    public required string Region { get; set; }

    /// <summary>
    /// Postal or ZIP code
    /// </summary>
    /// <example>90210</example>
    [Required]
    [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    [JsonPropertyName("postal_code")]
    [JsonPropertyOrder(13)]
    public required string PostalCode { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    /// <example>United States</example>
    [Required]
    [MaxLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
    [JsonPropertyName("country")]
    [JsonPropertyOrder(14)]
    public required string Country { get; set; }

    /// <summary>
    /// Geographic location
    /// </summary>
    [Required]
    [JsonPropertyName("location")]
    [JsonPropertyOrder(15)]
    public required LocationPoint Location { get; set; }

    /// <summary>
    /// IANA time zone identifier
    /// </summary>
    /// <example>America/Los_Angeles</example>
    [Required]
    [MaxLength(32, ErrorMessage = "Time zone identifier cannot exceed 32 characters")]
    [JsonPropertyName("time_zone_id")]
    [JsonPropertyOrder(16)]
    public required string TimeZoneId { get; set; }

    /// <summary>
    /// Business hours for the venue
    /// </summary>
    [JsonPropertyName("business_hours")]
    [JsonPropertyOrder(17)]
    public List<BusinessHoursItem> BusinessHours { get; set; } = new List<BusinessHoursItem>();

    /// <summary>
    /// Indicates whether this venue is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(18)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp when this venue was created
    /// </summary>
    /// <example>2025-07-23T08:00:00Z</example>
    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(998)]
    public Instant CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when this venue was last updated
    /// </summary>
    /// <example>2025-07-23T08:00:00Z</example>
    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(999)]
    public Instant? UpdatedAt { get; set; }
}
