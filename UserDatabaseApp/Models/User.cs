using System.ComponentModel.DataAnnotations;
using UserDatabaseApp.Services;

namespace UserDatabaseApp.Models;

public class User
{
    public string TimeCreated { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "First name is required")]
    [StringLength(
        50,
        MinimumLength = 2,
        ErrorMessage = "First name must be between 2 and 50 characters"
    )]
    [RegularExpression(
        @"^[a-zA-ZåäöÅÄÖ\s-]*$",
        ErrorMessage = "First name can only contain letters, spaces, and hyphens"
    )]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(
        50,
        MinimumLength = 2,
        ErrorMessage = "Last name must be between 2 and 50 characters"
    )]
    [RegularExpression(
        @"^[a-zA-ZåäöÅÄÖ\s-]*$",
        ErrorMessage = "Last name can only contain letters, spaces, and hyphens"
    )]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(40, ErrorMessage = "Email cannot be longer than 40 characters")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Invalid phone number")]
    [RegularExpression(
        @"^[\d\s+-]*$",
        ErrorMessage = "Phone number can only contain digits, spaces, plus signs, and hyphens"
    )]
    [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters")]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(40, ErrorMessage = "Address cannot be longer than 40 characters")]
    public string Address { get; set; } = null!;

    [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Postal code must be in format: XXX XX")]
    [StringLength(6, ErrorMessage = "Postal code cannot be longer than 6 characters")]
    public string PostalCode { get; set; } = null!;

    [StringLength(20, ErrorMessage = "City cannot be longer than 20 characters")]
    [RegularExpression(
        @"^[a-zA-ZåäöÅÄÖ\s-]*$",
        ErrorMessage = "City can only contain letters, spaces, and hyphens"
    )]
    public string City { get; set; } = null!;
}
