using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class Habit
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool? Deleted { get; set; }
        [Required]
        public string? HabitName { get; set; }
        [Required]
        public DateOnly CreationDate { get; set; }
    }
}
