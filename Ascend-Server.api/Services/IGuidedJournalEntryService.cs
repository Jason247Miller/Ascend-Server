  using Models; 

  public interface IGuidedJournalEntryService
    {      
        List<GuidedJournalEntry> GetAllForUserId(int userId);
        void Add(GuidedJournalEntry guidedJournalEntry);
        void Update(GuidedJournalEntry guidedJournalEntry);
       
    }