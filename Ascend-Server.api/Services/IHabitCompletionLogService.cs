  using Models; 

  public interface IHabitCompletionLogService
    {      
        List<HabitCompletionLog> GetAllForUserId(int userId);
        void Add (HabitCompletionLog habitCompletionLog); 
        void Update(HabitCompletionLog habitCompletionLog);
    }