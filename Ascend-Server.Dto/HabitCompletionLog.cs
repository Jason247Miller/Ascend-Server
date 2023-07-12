using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class HabitCompletionLog
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid HabitId { get; set; }
        public bool? Completed { get; set; }
        [Required]
        public DateOnly? Date { get; set; }
    }
}
