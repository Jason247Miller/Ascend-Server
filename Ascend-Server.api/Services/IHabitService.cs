  using Models; 

  public interface IHabitService
    {      
        List<Habit> GetAllForUserId(int userId);
        void Add (Habit habit);
        void Update(Habit habit);
    }