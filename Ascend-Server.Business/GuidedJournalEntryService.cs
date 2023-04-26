using Data;
using Data.Exceptions;
using IServices;

namespace Services;

public class GuidedJournalEntryService : IGuidedJournalEntryService
{
    private readonly ApiContext _apiContext;

    public GuidedJournalEntryService(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }
    public GuidedJournalEntry GetById(Guid id)
    {
        var existingEntry = _apiContext.GuidedJournalEntries.SingleOrDefault(gje => gje.Id == id);

        if (existingEntry == null)
        {
            throw new NotFoundException("Guided Journal Entry");
        }

        return existingEntry;
    }
    public IEnumerable<Data.GuidedJournalEntry> GetAllForUserId(Guid userId)
    {
        var userEntries = _apiContext.GuidedJournalEntries.Where(gje => gje.UserId == userId);

        return userEntries;
    }

    public void Add(GuidedJournalEntry guidedJournalEntryPassed)
    {
        var guidedJournalLog = new GuidedJournalLog
        {
            Id = Guid.NewGuid(),
            UserId = guidedJournalEntryPassed.UserId,
            EntryId = guidedJournalEntryPassed.Id,
            EntryTextValue = "",
            Date = guidedJournalEntryPassed.CreationDate

        };

        _apiContext.GuidedJournalEntries.Add(guidedJournalEntryPassed);

        _apiContext.GuidedJournalLogs.Add(guidedJournalLog);

        _apiContext.SaveChanges();

    }

    public void Update(GuidedJournalEntry guidedJournalEntryPassed, Guid id)
    {
        var existingJournalEntry = _apiContext.GuidedJournalEntries.SingleOrDefault(gje => gje.Id == id &&
                                                                                    gje.UserId == guidedJournalEntryPassed.UserId);

        if (existingJournalEntry == null)
        {
            throw new NotFoundException("Journal Entry");
        }
        else
        {
            existingJournalEntry.EntryName = guidedJournalEntryPassed.EntryName;

            _apiContext.SaveChanges();
        }

    }

    public void Delete(Guid id)
    {
        var existingJournalEntry = _apiContext.GuidedJournalEntries.SingleOrDefault(gje => gje.Id == id);

        if (existingJournalEntry == null)
        {
            throw new NotFoundException("Journal Entry");
        }

            existingJournalEntry.Deleted = true;

            _apiContext.SaveChanges();
    }
}