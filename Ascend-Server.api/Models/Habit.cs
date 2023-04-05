namespace Models;

using Ascend_Server.api.IServices;
using System.ComponentModel.DataAnnotations;

public class Habit 
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public bool? Deleted { get; set; }
    public string? HabitName { get; set; }
    [Required]
    public DateOnly CreationDate { get; set; }
}