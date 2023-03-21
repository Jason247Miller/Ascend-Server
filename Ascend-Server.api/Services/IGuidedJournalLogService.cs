 using Models; 

  public interface IGuidedJournalLogService
    {      
        List<GuidedJournalLog> GetAllForUserId(Guid userId);
        void Add (GuidedJournalLog guidedJournalLog); 
        void Update(GuidedJournalLog guidedJournalLog);
    }