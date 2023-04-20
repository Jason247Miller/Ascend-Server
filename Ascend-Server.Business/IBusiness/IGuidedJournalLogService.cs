using Data;

namespace IServices;

public interface IGuidedJournalLogService
{
    IEnumerable<Data.GuidedJournalLog> GetAllForUserId(Guid userId);
    GuidedJournalLog GetById(Guid id);
    void Add(GuidedJournalLog guidedJournalLog);
    void Update(GuidedJournalLog guidedJournalLog, Guid id);
}