using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class HabitCompletionLogForCreation
    {
        public string? Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid HabitId { get; set; }
        public bool? Completed { get; set; }
        [Required]
        public DateOnly? Date { get; set; }
    }
}
