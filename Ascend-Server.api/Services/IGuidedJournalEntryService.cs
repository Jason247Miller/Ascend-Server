  using Models; 

  public interface IGuidedJournalEntryService
    {      
        List<GuidedJournalEntry> GetAllForUserId(int userId);
        void Add(GuidedJournalEntry guidedJournalEntry);
        bool EntryExistsAndNotDeleted(string? entryIdPassed, int userIdPassed);
        void Update(GuidedJournalEntry guidedJournalEntry);
       
    }