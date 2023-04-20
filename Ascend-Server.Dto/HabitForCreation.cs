using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class HabitForCreation
    {
        public string? Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool? Deleted { get; set; }
        public string? HabitName { get; set; }
        [Required]
        public DateOnly CreationDate { get; set; }
    }
}
