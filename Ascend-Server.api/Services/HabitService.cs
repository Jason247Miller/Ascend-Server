using Models; 
using Exceptions; 
using System;
using Services; 

namespace Services; 

public class HabitService:IHabitService
{

     private readonly ApiContext _apiContext; 

     public HabitService(
         IUserService userService,
         ApiContext apiContext)
     {
        _apiContext = apiContext;

     }

    public List<Habit> GetAllForUserId(int userIdPassed)
    {

      CheckUserId(userIdPassed); 
      
      List<Habit> userHabits = _apiContext.Habits.Where(h => h.UserId == userIdPassed).ToList();
    
      return userHabits; 
    }

    public void Add(Habit habit)
    {
        CheckUserId(habit.UserId); 

        var habitsForUser = _apiContext.Habits.Where(h => h.UserId == habit.UserId && h.Uuid == habit.Uuid);
        
        if(habitsForUser.Any())
        {
            throw new DuplicateHabitException();
        }
       
        _apiContext.Habits.Add(habit);
    }
    
    public void Update(Habit habit)
    {
        var existingHabit = _apiContext.Habits.SingleOrDefault(h => h.Id == habit.Id &&
                                                         h.UserId == habit.UserId);

        if(existingHabit == null)
        {  
            throw new HabitNotFoundException(); 
        }
        else
        {
            existingHabit.Deleted = habit.Deleted; 

            existingHabit.HabitName = habit.HabitName;

            _apiContext.SaveChanges();

        }
     
    }
    
    private void CheckUserId(int userIdPassed)
    {
        var userId = _apiContext.Users.SingleOrDefault(u => u.Id == userIdPassed);

        if (userId == null)
        {        
            throw new UserDoesNotExistException();
        }
    }
   
}