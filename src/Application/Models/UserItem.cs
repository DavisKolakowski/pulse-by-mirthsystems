using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;


/// <summary>
/// Basic user information
/// </summary>
public class UserItem : ItemBase
{
    /// <summary>
    /// The first name of the user
    /// </summary>
    /// <example>John</example>
    [Required]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    [JsonPropertyName("first_name")]
    [JsonPropertyOrder(1)]
    public required string FirstName { get; set; }

    /// <summary>
    /// The last name of the user
    /// </summary>
    /// <example>Doe</example>
    [Required]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    [JsonPropertyName("last_name")]
    [JsonPropertyOrder(2)]
    public required string LastName { get; set; }

    /// <summary>
    /// The email address of the user
    /// </summary>
    /// <example>john.doe@example.com</example>
    [Required]
    [EmailAddress]
    [MaxLength(254, ErrorMessage = "Email cannot exceed 254 characters")]
    [JsonPropertyName("email_address")]
    [JsonPropertyOrder(3)]
    public required string EmailAddress { get; set; }
}