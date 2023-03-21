  using Models; 

  public interface IHabitCompletionLogService
    {      
        List<HabitCompletionLog> GetAllForUserId(Guid userId);
        void Add (HabitCompletionLog habitCompletionLog); 
        void Update(HabitCompletionLog habitCompletionLogPassed);
    }