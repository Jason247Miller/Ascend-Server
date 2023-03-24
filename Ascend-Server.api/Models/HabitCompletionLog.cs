namespace Models;

using System.ComponentModel.DataAnnotations;

public class HabitCompletionLog
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    [RegularExpression(@"\b[A-Fa-f0-9]{8}(?:-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\b")]
    public Guid HabitId { get; set; }
    public bool? Completed { get; set; }
    public DateOnly? Date { get; set; }
}