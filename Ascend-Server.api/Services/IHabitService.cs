  using Models; 

  public interface IHabitService
    {      
        List<Habit> GetAllForUserId(Guid userId);
        void Add (Habit habit);
        void Update(Habit habit);
    }