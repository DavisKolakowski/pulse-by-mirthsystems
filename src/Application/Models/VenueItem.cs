using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using NodaTime;

using Xunit.Sdk;

namespace Application.Models;


/// <summary>
/// Base venue information
/// </summary>
public class VenueItem : ItemBase
{
    /// <summary>
    /// The name of the venue
    /// </summary>
    /// <example>The Blue Oyster Bar</example>
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    /// <summary>
    /// Optional description of the venue
    /// </summary>
    /// <example>A cozy neighborhood bar with live music every weekend</example>
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    /// <summary>
    /// The phone number of the venue
    /// </summary>
    /// <example>+1-555-123-4567</example>
    [Phone]
    [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    [JsonPropertyName("phone_number")]
    [JsonPropertyOrder(3)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// The website URL of the venue
    /// </summary>
    /// <example>https://www.blueoysterbar.com</example>
    [Url]
    [MaxLength(200, ErrorMessage = "Website URL cannot exceed 200 characters")]
    [JsonPropertyName("website")]
    [JsonPropertyOrder(4)]
    public string? Website { get; set; }

    /// <summary>
    /// The email address of the venue
    /// </summary>
    /// <example>info@blueoysterbar.com</example>
    [EmailAddress]
    [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [JsonPropertyName("email")]
    [JsonPropertyOrder(5)]
    public string? Email { get; set; }

    /// <summary>
    /// Profile image URL for the venue
    /// </summary>
    /// <example>https://images.example.com/venue-123.jpg</example>
    [Url]
    [MaxLength(500, ErrorMessage = "Profile image URL cannot exceed 500 characters")]
    [JsonPropertyName("profile_image")]
    [JsonPropertyOrder(6)]
    public string? ProfileImage { get; set; }

    /// <summary>
    /// Street address of the venue
    /// </summary>
    /// <example>123 Main Street</example>
    [Required]
    [MaxLength(200, ErrorMessage = "Street address cannot exceed 200 characters")]
    [JsonPropertyName("street_address")]
    [JsonPropertyOrder(7)]
    public required string StreetAddress { get; set; }

    /// <summary>
    /// Secondary address line
    /// </summary>
    /// <example>Suite 100</example>
    [MaxLength(100, ErrorMessage = "Secondary address cannot exceed 100 characters")]
    [JsonPropertyName("secondary_address")]
    [JsonPropertyOrder(8)]
    public string? SecondaryAddress { get; set; }

    /// <summary>
    /// City or locality
    /// </summary>
    /// <example>Seattle</example>
    [Required]
    [MaxLength(100, ErrorMessage = "Locality cannot exceed 100 characters")]
    [JsonPropertyName("locality")]
    [JsonPropertyOrder(9)]
    public required string Locality { get; set; }

    /// <summary>
    /// State or region
    /// </summary>
    /// <example>WA</example>
    [Required]
    [MaxLength(50, ErrorMessage = "Region cannot exceed 50 characters")]
    [JsonPropertyName("region")]
    [JsonPropertyOrder(10)]
    public required string Region { get; set; }

    /// <summary>
    /// Postal or ZIP code
    /// </summary>
    /// <example>98101</example>
    [Required]
    [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    [JsonPropertyName("postal_code")]
    [JsonPropertyOrder(11)]
    public required string PostalCode { get; set; }

    /// <summary>
    /// Country
    /// </summary>
    /// <example>USA</example>
    [Required]
    [MaxLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
    [JsonPropertyName("country")]
    [JsonPropertyOrder(12)]
    public required string Country { get; set; }

    /// <summary>
    /// Longitude coordinate
    /// </summary>
    /// <example>-122.3321</example>
    [Required]
    [JsonPropertyName("longitude")]
    [JsonPropertyOrder(13)]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
    public double Longitude { get; set; }

    /// <summary>
    /// Latitude coordinate
    /// </summary>
    /// <example>47.6062</example>
    [Required]
    [JsonPropertyName("latitude")]
    [JsonPropertyOrder(14)]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
    public double Latitude { get; set; }

    /// <summary>
    /// IANA time zone identifier
    /// </summary>
    /// <example>America/Los_Angeles</example>
    [Required]
    [MaxLength(32, ErrorMessage = "Time zone identifier cannot exceed 32 characters")]
    [JsonPropertyName("time_zone_id")]
    [JsonPropertyOrder(15)]
    public required string TimeZoneId { get; set; }

    /// <summary>
    /// Indicates whether this venue is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(16)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The primary category for this venue
    /// </summary>
    [JsonPropertyName("primary_category")]
    [JsonPropertyOrder(17)]
    public VenueCategoryItem? PrimaryCategory { get; set; }

    /// <summary>
    /// The secondary category for this venue
    /// </summary>
    [JsonPropertyName("secondary_category")]
    [JsonPropertyOrder(18)]
    public VenueCategoryItem? SecondaryCategory { get; set; }

    /// <summary>
    /// Timestamp when this venue was created
    /// </summary>
    /// <example>2025-07-21T16:51:17Z</example>
    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(998)]
    public Instant CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when this venue was last updated
    /// </summary>
    /// <example>2025-07-21T16:51:17Z</example>
    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(999)]
    public Instant? UpdatedAt { get; set; }
}
