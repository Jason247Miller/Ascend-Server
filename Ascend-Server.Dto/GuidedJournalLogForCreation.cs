using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class GuidedJournalLogForCreation
    {
        public string? Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid EntryId { get; set; }
        public string? EntryTextValue { get; set; }
        [Required]
        public DateOnly Date { get; set; }
    }
}
