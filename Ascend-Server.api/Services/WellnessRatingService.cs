using Models;
using Exceptions;

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

    public WellnessRating[] GetAllForUserId(Guid userId)
    {
        _userService.CheckUserId(userId);

        var userWellnessRatings = _apiContext.WellnessRatings.Where(r => r.UserId == userId).ToArray();

        return userWellnessRatings;
    }

    public void Update(WellnessRating wellnessRatingPassed, Guid id)
    {
        _userService.CheckUserId(wellnessRatingPassed.UserId);

        var existingRating = _apiContext.WellnessRatings.FirstOrDefault(
            wr => wr.Id == id &&
            wr.UserId == wellnessRatingPassed.UserId
            );

        if (existingRating == null)
        {
            throw new NotFoundException("Wellness Rating");
        }
        else
        {
            _apiContext.Add(wellnessRatingPassed);

            _apiContext.SaveChanges();
        }

    }

}
