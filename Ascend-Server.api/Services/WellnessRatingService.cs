using Models;
using Exceptions;
using System;
using Services;

namespace Services;

public class WellnessRatingService : IWellnessRatingService
{

    private readonly IUserService _userService;

    private readonly ApiContext _apiContext;

    public WellnessRatingService(IUserService userService,
                                 ApiContext apiContext)
    {
        _userService = userService;

        _apiContext = apiContext;
    }

    public void Add(WellnessRating wellnessRatingPassed)
    {
        _userService.CheckUserId(wellnessRatingPassed.UserId);

        var existsForSameDate = _apiContext.WellnessRatings.Where(wr => wr.UserId == wellnessRatingPassed.UserId && wr.Date == wellnessRatingPassed.Date);

        if (existsForSameDate.Any())
        {
            throw new SameDateException("Wellness Rating", wellnessRatingPassed.Date.ToString() ?? "");
        }

        _apiContext.WellnessRatings.Add(wellnessRatingPassed);

        _apiContext.SaveChanges();
    }

    public List<WellnessRating> GetAllForUserId(int userId)
    {
        _userService.CheckUserId(userId);

        List<WellnessRating> userWellnessRatings = _apiContext.WellnessRatings.Where(r => r.UserId == userId).ToList();

        return userWellnessRatings;
    }

    public void Update(WellnessRating wellnessRatingPassed)
    {
        _userService.CheckUserId(wellnessRatingPassed.UserId);

        var existingRating = _apiContext.WellnessRatings.FirstOrDefault(
            wr => wr.Id == wellnessRatingPassed.Id &&
            wr.UserId == wellnessRatingPassed.UserId
            );

        if (existingRating == null)
        {
            throw new NotFoundException("Wellness Rating");
        }
        else
        {
            existingRating.SleepRating = wellnessRatingPassed.SleepRating;
            existingRating.ExerciseRating = wellnessRatingPassed.ExerciseRating;
            existingRating.NutritionRating = wellnessRatingPassed.NutritionRating;
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
