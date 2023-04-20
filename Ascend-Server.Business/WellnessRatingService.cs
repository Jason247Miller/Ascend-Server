using Data;
using Data.Exceptions;
using IServices;

namespace Services;

public class WellnessRatingService : IWellnessRatingService
{

    private readonly ApiContext _apiContext;

    public WellnessRatingService(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }

    public void Add(WellnessRating wellnessRatingPassed)
    {

        var existsForSameDate = _apiContext.WellnessRatings.Where(wr => wr.UserId == wellnessRatingPassed.UserId &&
                                                                        wr.Date == wellnessRatingPassed.Date);

        if (existsForSameDate.Any())
        {
            throw new SameDateException("Wellness Rating", wellnessRatingPassed.Date.ToString() ?? "");
        }

        _apiContext.WellnessRatings.Add(wellnessRatingPassed);

        _apiContext.SaveChanges();
    }

    public WellnessRating GetById(Guid id)
    {    
        var existingRating = _apiContext.WellnessRatings.SingleOrDefault(wr => wr.Id == id);

        if (existingRating == null)
        {
            throw new NotFoundException("Wellness Rating");
        }

        return existingRating; 
    }
    public IEnumerable<WellnessRating> GetAllForUserId(Guid userId)
    {

        var userWellnessRatings = _apiContext.WellnessRatings.Where(r => r.UserId == userId);

        return userWellnessRatings;
    }

    public void Update(WellnessRating wellnessRatingPassed, Guid id)
    {
       
        var existingRating = _apiContext.WellnessRatings.SingleOrDefault(
            wr => wr.Id == id &&
            wr.UserId == wellnessRatingPassed.UserId
            );

        if (existingRating == null)
        {
            throw new NotFoundException("Wellness Rating");
        }
        else
        {      
            existingRating.Date = wellnessRatingPassed.Date;
            existingRating.SleepRating = wellnessRatingPassed.SleepRating;
            existingRating.ExerciseRating = wellnessRatingPassed.ExerciseRating;
            existingRating.NutritionRating = wellnessRatingPassed.NutritionRating;
            existingRating.Date = wellnessRatingPassed.Date;
            existingRating.StressRating = wellnessRatingPassed.StressRating;
            existingRating.SunlightRating = wellnessRatingPassed.SunlightRating;
            existingRating.MindfulnessRating = wellnessRatingPassed.MindfulnessRating;
            existingRating.ProductivityRating = wellnessRatingPassed.ProductivityRating;
            existingRating.MoodRating = wellnessRatingPassed.MoodRating;
            existingRating.EnergyRating = wellnessRatingPassed.EnergyRating;
            existingRating.OverallDayRating = wellnessRatingPassed.OverallDayRating;

            _apiContext.SaveChanges();
           
        }

    }

}
