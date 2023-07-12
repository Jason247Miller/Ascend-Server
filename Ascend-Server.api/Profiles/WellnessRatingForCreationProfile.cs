using Dto;
using AutoMapper;

namespace profiles;

public class WellnessRatingForCreationProfile : Profile
{
    public WellnessRatingForCreationProfile()
    {
        CreateMap<WellnessRatingForCreation, Data.WellnessRating>()
            .ForMember(
            wr => wr.Id,
            options => options.MapFrom(newId => Guid.NewGuid())
            )
            .ForMember(
            wr => wr.UserId,
            options => options.MapFrom(src => src.UserId)
            )
            .ForMember(
            wr => wr.Date,
            options => options.MapFrom(src => src.Date)
            )
            .ForMember(
            wr => wr.SleepRating,
            options => options.MapFrom(src => src.SleepRating)
            )
            .ForMember(
            wr => wr.ExerciseRating,
            options => options.MapFrom(src => src.ExerciseRating)
            )
            .ForMember(
            wr => wr.NutritionRating,
            options => options.MapFrom(src => src.NutritionRating)
            )
            .ForMember(
            wr => wr.StressRating,
            options => options.MapFrom(src => src.StressRating)
            )
            .ForMember(
            wr => wr.SunlightRating,
            options => options.MapFrom(src => src.SunlightRating)
            )
             .ForMember(
            wr => wr.MindfulnessRating,
            options => options.MapFrom(src => src.MindfulnessRating)
            )
            .ForMember(
            wr => wr.ProductivityRating,
            options => options.MapFrom(src => src.ProductivityRating)
            )
            .ForMember(
            wr => wr.MoodRating,
            options => options.MapFrom(src => src.MoodRating)
            )
            .ForMember(
            wr => wr.EnergyRating,
            options => options.MapFrom(src => src.EnergyRating)
            )
            .ForMember(
            wr => wr.OverallDayRating,
            options => options.MapFrom(src => src.OverallDayRating)
            );
    }
}



