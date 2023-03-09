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
                date = "03-02-2023",
                sleepRating = 7, 
                exerciseRating = 5, 
                nutritionRating = 2, 
                stressRating = 9, 
                sunlightRating = 4, 
                mindfulnessRating = 2, 
                productivityRating = 9, 
                moodRating = 9, 
                energyRating = 9, 
                overallDayRating = 4
            },

            new WellnessRating {
                Id = 2,
                UserId = 2,
                date = "03-01-2023",
                sleepRating = 7, 
                exerciseRating = 5, 
                nutritionRating = 2, 
                stressRating = 9, 
                sunlightRating = 4, 
                mindfulnessRating = 2, 
                productivityRating = 9, 
                moodRating = 9, 
                energyRating = 9, 
                overallDayRating = 4
            }

         
        };
    }

    public static List<WellnessRating> GetAll() => WellnessRatings;
   
    public static WellnessRating? Get(int id) => WellnessRatings.FirstOrDefault(wr => wr.Id == id);

    public static void Add(WellnessRating wellnessRating)
    {
        //check if there is already an existing entry for the sent date and current user
        var existsForSameDate = WellnessRatings.Where(wr => wr.UserId == wellnessRating.UserId && wr.date == wellnessRating.date);
        if(existsForSameDate.Any())
        {
            throw new DuplicateWellnessRatingException(wellnessRating.date);
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
