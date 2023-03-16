using Models; 
using Exceptions; 
using System;
using Services; 
namespace Services; 

public class HabitService:IHabitService
{
     List<Habit> Habits { get; }
     int nextId = 3;
     private readonly IUserService _userService; 
     public HabitService(IUserService userService)
    {
        _userService = userService;//used to check user id, may not be needed later?
        Habits = new List<Habit>
        {
            new Habit {
                Id = 1,
                UserId = 1,
                HabitName = "20 minutes of cardio",
                Uuid = "d58a9560-3ed8-4eaa-b97e-c558179861e8", 
                Deleted = false           
            },

            new Habit {
                Id = 2,
                UserId = 1,
                HabitName = "Learned 1 new thing",
                Uuid = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec", 
                Deleted = false           
            }

        };
    }

    public List<Habit> GetAllForUserId(int userId)
    {
      if(_userService.CheckUser(userId) == null)
      {
         throw new Exception("Invalid Request");  
      }

      List<Habit> userHabits = Habits.Where(h => h.UserId == userId).ToList();
      return userHabits; 
    }

    public void Add(Habit habit)
    {
         if(_userService.CheckUser(habit.UserId) == null)
        {
         throw new UserDoesNotExistException(habit.UserId);  
        }
        var habitsForUser = Habits.Where(h => h.UserId == habit.UserId && h.Uuid == habit.Uuid);
        
        if(habitsForUser.Any())
        {   
            throw new Exception("Bad Request: Unable to Create new Habit due to UUID Duplication");
        }
       
        habit.Id = nextId++;
        Habits.Add(habit);
    }

    public void Update(Habit habit)
    {
        var index = Habits.FindIndex(
            h => h.Id == habit.Id &&
            h.UserId == habit.UserId
            );
       
        if(index == -1)
        {  
            throw new NotFoundException(habit); 
        }
        else
        {
            Habits[index] = habit;
        }
     
    }
    public bool HabitExistsAndNotDeleted(string? habitIdPassed, int userIdPassed)
    {
       return Habits.Any(h => h.Uuid == habitIdPassed && h.Deleted == false && h.UserId == userIdPassed);
        
    }
   
}