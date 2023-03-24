  using Models; 

  public interface IHabitService
    {      
        List<Habit> GetAllForUserId(int userId);
        void Add (Habit habit);
        bool HabitExistsAndNotDeleted(string? habitIdPassed, int userIdPassed);
        void Update(Habit habit);
    }