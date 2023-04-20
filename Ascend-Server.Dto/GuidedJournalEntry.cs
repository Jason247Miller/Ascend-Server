using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class GuidedJournalEntry
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid? UserId { get; set; }
        public string? EntryName { get; set; }
        [Required]
        public bool? Deleted { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}




