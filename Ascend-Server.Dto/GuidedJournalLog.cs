using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class GuidedJournalLog
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid EntryId { get; set; }
        public string? EntryTextValue { get; set; }
        [Required]
        public DateOnly? Date { get; set; }
    }
}
