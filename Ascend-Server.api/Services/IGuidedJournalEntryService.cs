  using Models; 

  public interface IGuidedJournalEntryService
    {      
        List<GuidedJournalEntry> GetAll(Guid userId);
        void Add(GuidedJournalEntry guidedJournalEntry);
        void Update(GuidedJournalEntry guidedJournalEntry);
       
    }