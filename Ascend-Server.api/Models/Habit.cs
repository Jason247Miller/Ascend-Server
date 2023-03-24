namespace Models;

using System.ComponentModel.DataAnnotations;

public class Habit
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    [RegularExpression(@"\b[A-Fa-f0-9]{8}(?:-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\b")]
    public bool? Deleted { get; set; }
    public string? HabitName { get; set; }
    [Required]
    public DateOnly CreationDate { get; set; }
}