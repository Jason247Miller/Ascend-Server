 using Models; 

  public interface IGuidedJournalLogService
    {      
        List<GuidedJournalLog> GetAllForUserId(int userId);
        void Add (GuidedJournalLog guidedJournalLog); 
        void Update(GuidedJournalLog guidedJournalLog);
    }