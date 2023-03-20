using Models; 
using Exceptions; 
using System;
using Services; 
namespace Services; 

public class HabitCompletionLogService:IHabitCompletionLogService
{
     List<HabitCompletionLog> HabitCompletionLogs {get;}

     int nextId = 3;

     private readonly IUserService _userService; 
     private readonly IHabitService _habitService; 

     public HabitCompletionLogService(
        IUserService userService,
        IHabitService habitService
    )
    {
        _userService = userService;
        _habitService = habitService; 

        HabitCompletionLogs = new List<HabitCompletionLog>
        {
            new HabitCompletionLog {
                Id = 1,
                UserId = 1,
                HabitId = "d58a9560-3ed8-4eaa-b97e-c558179861e8", 
                Completed = true,
                Date = new DateOnly(2023, 3, 16)         
            },

            new HabitCompletionLog {
                Id = 1,
                UserId = 1,
                HabitId = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec", 
                Completed = true,
                Date = new DateOnly(2023, 3, 16)
            }
           
        };
    }

    public List<HabitCompletionLog> GetAllForUserId(int userId)
    {
      if(_userService.CheckUser(userId) == null)
      {
         throw new Exception("Invalid Request");  
      }

      List<HabitCompletionLog> userHabitCompletionLogs = HabitCompletionLogs.Where(hcl => hcl.UserId == userId).ToList();
      return userHabitCompletionLogs; 
    }

    public void Add(HabitCompletionLog habitCompletionLog)
    {
         if(_userService.CheckUser(habitCompletionLog.UserId) == null)
        {
         throw new UserDoesNotExistException();  
        }
       //var habitFound = _habitService.HabitExistsAndNotDeleted(habitCompletionLog.HabitId, habitCompletionLog.UserId);
       
      // if(!habitFound)
      // {
      //  throw new HabitNotFoundException();
      // }
        
        habitCompletionLog.Id = nextId++;
        HabitCompletionLogs.Add(habitCompletionLog);
    }

    public void Update(HabitCompletionLog habitCompletionLog)
    {
        
        var index = HabitCompletionLogs.FindIndex(
            hcl => hcl.Id == habitCompletionLog.Id &&
            hcl.UserId == habitCompletionLog.UserId
            );
       
        if(index == -1)
        {  
            throw new NotFoundException(habitCompletionLog); 
        }
        else
        {
            HabitCompletionLogs[index] = habitCompletionLog;
        }
     
    }
   
}