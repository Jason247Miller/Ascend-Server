using Models;
using Exceptions;

namespace Services;

public class GuidedJournalEntryService : IGuidedJournalEntryService
{
    private readonly IUserService _userService;

    private readonly ApiContext _apiContext;

    public GuidedJournalEntryService(IUserService userService,
                                     ApiContext apiContext)
    {
        _apiContext = apiContext;

        _userService = userService;
    }

    public GuidedJournalEntry[] GetAllForUserId(Guid userId)
    {
        _userService.CheckUserId(userId);

        var userEntries = _apiContext.GuidedJournalEntries.Where(gje => gje.UserId == userId).ToArray();

        return userEntries;
    }

    public void Add(GuidedJournalEntry guidedJournalEntryPassed)
    {
        _userService.CheckUserId(guidedJournalEntryPassed.UserId);

        var entriesForUser = _apiContext.GuidedJournalEntries.FirstOrDefault(gje => gje.UserId == guidedJournalEntryPassed.UserId && gje.Id == gje.Id);

        if (entriesForUser == null)
        {
            Console.WriteLine("duplicate entries");
            throw new DuplicateEntryException();
        }

        _apiContext.GuidedJournalEntries.Add(guidedJournalEntryPassed);

        _apiContext.SaveChanges();
    }

    public void Update(GuidedJournalEntry guidedJournalEntryPassed, Guid id)
    {
        var existingJournalEntry = _apiContext.GuidedJournalEntries.FirstOrDefault(gje => gje.Id == id &&
                                                                                   gje.UserId == guidedJournalEntryPassed.UserId);

        if (existingJournalEntry == null)
        {
            throw new NotFoundException("Journal Entry");
        }
        else
        {
            existingJournalEntry.EntryName = guidedJournalEntryPassed.EntryName;

            existingJournalEntry.Deleted = guidedJournalEntryPassed.Deleted;

            _apiContext.SaveChanges();
        }

    }

}