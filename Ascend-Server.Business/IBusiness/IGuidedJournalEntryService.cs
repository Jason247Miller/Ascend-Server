using Data;

namespace IServices;

public interface IGuidedJournalEntryService
{
    IEnumerable<GuidedJournalEntry> GetAllForUserId(Guid userId);
    GuidedJournalEntry GetById(Guid id);
    void Add(GuidedJournalEntry guidedJournalEntry);
    void Update(GuidedJournalEntry guidedJournalEntry, Guid id);
    public void Delete(Guid id); 
}
