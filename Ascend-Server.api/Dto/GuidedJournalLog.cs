namespace Ascend_Server.api.Dto
{
    public class GuidedJournalLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EntryId { get; set; }
        public string? EntryTextValue { get; set; }
        public DateOnly? Date { get; set; }
    }
}

