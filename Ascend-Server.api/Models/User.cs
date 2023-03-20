using System.ComponentModel.DataAnnotations;
namespace Models; 

public class User
{
    public int Id {get; set;}
    [Required]
    public string? FirstName {get; set;}
    [Required]
    public string? LastName {get; set;}
    [Required]
    [EmailAddress]
    public string? Email {get; set;}
    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9])(?=.*[a-z]).{8,}$", 
        ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and must be at least 8 characters long.")]
    public string? Password {get; set;}
}