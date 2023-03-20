using Models; 
using Exceptions; 
using System;
using Services; 
namespace Services; 

public class WellnessRatingService:IWellnessRatingService
{
     List<WellnessRating> WellnessRatings { get; }
     int nextId = 3;
     private readonly IUserService _userService; 
     public WellnessRatingService(IUserService userService)
    {
        _userService = userService; 
        WellnessRatings = new List<WellnessRating>
        {
            new WellnessRating {
                Id = 1,
                UserId = 1,
                Date =  new DateOnly(2023, 3, 16),
                SleepRating = 7, 
                ExerciseRating = 5, 
                NutritionRating = 2, 
                StressRating = 9, 
                SunlightRating = 4, 
                MindfulnessRating = 2, 
                ProductivityRating = 9, 
                MoodRating = 9, 
                EnergyRating = 9, 
                OverallDayRating = 4
            },

            new WellnessRating {
                Id = 2,
                UserId = 2,
                Date =  new DateOnly(2023, 3, 16),
                SleepRating = 7, 
                ExerciseRating = 5, 
                NutritionRating = 2, 
                StressRating = 9, 
                SunlightRating = 4, 
                MindfulnessRating = 2, 
                ProductivityRating = 9, 
                MoodRating = 9, 
                EnergyRating = 9, 
                OverallDayRating = 4
            }

         
        };
    }

    public List<WellnessRating> GetAll() => WellnessRatings;
   
    public WellnessRating? Get(int id) => WellnessRatings.FirstOrDefault(wr => wr.Id == id);

    public void Add(WellnessRating wellnessRating)
    {   
        if(_userService.CheckUser(wellnessRating.UserId) == null)
        {
         throw new UserDoesNotExistException();  
        }
        var existsForSameDate = WellnessRatings.Where(wr => wr.UserId == wellnessRating.UserId && wr.Date == wellnessRating.Date);
        
        if(existsForSameDate.Any())
        {   
            throw new DuplicateWellnessRatingException(wellnessRating.Date);
        }
       
        wellnessRating.Id = nextId++;
        WellnessRatings.Add(wellnessRating);
    }

     public  List<WellnessRating> GetAllForUserId(int userId)
    {
      if(_userService.CheckUser(userId) == null)
      {
         throw new UserDoesNotExistException();  
      }

      List<WellnessRating> userWellnessRatings = WellnessRatings.Where(r => r.UserId == userId).ToList();
      return userWellnessRatings; 
    }

    public  void Update(WellnessRating wellnessRating)
    {
        var index = WellnessRatings.FindIndex(
            wr => wr.Id == wellnessRating.Id &&
            wr.UserId == wellnessRating.UserId
            );
       
        if(index == -1)
        {  
            throw new NotFoundException(wellnessRating); 
        }
        else
        {
            WellnessRatings[index] = wellnessRating;
        }
     
    }


}
