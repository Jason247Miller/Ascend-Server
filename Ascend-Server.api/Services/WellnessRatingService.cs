using Models; 
using Exceptions; 
using System; 
namespace Services; 

public static class WellnessRatingService
{
    static List<WellnessRating> WellnessRatings { get; }
    static int nextId = 3;
    static WellnessRatingService()
    {
        WellnessRatings = new List<WellnessRating>
        {
            new WellnessRating {
                Id = 1,
                UserId = 1,
                Date =  new DateTime(2023, 3, 2),
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
                Date =  new DateTime(2023, 3, 1),
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

    public static List<WellnessRating> GetAll() => WellnessRatings;
   
    public static WellnessRating? Get(int id) => WellnessRatings.FirstOrDefault(wr => wr.Id == id);

    public static void Add(WellnessRating wellnessRating)
    {   
        var existsForSameDate = WellnessRatings.Where(wr => wr.UserId == wellnessRating.UserId && wr.Date.Date == wellnessRating.Date.Date);
        
        if(existsForSameDate.Any())
        {   
            throw new DuplicateWellnessRatingException(wellnessRating.Date);
        }
        //will have to verify userId is valid at some point
        //if userId does not exist, record should not be added
        wellnessRating.Id = nextId++;
        WellnessRatings.Add(wellnessRating);
    }

     public static List<WellnessRating> GetAllForUserId(int userId)
    {
      List<WellnessRating> userWellnessRatings = WellnessRatings.Where(r => r.UserId == userId).ToList();
      return userWellnessRatings; 
    }

    public static void Update(WellnessRating wellnessRating)
    {
        var index = WellnessRatings.FindIndex(wr => wr.Id == wr.Id);
       
        if(index == 0)
        {  
            throw new NotFoundException(wellnessRating); 
        }
        else
        {
            WellnessRatings[index] = wellnessRating;
        }
     
    }
}
