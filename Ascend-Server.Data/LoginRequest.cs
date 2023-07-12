namespace Data;

using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9])(?=.*[a-z]).{8,}$",
        ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and must be at least 8 characters long.")
    ]
    public string? Password { get; set; }
}