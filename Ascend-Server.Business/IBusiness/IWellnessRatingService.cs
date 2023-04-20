using Data;

namespace IServices;

public interface IWellnessRatingService
{
    void Add(WellnessRating wellnessRating);
    WellnessRating GetById(Guid id);
    IEnumerable<Data.WellnessRating> GetAllForUserId(Guid userId);
    void Update(WellnessRating wellnessRating, Guid id);
}