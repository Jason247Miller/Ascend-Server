using Dto;
using AutoMapper;

namespace profiles;

public class WellnessRatingProfile : Profile
{
    public WellnessRatingProfile()
    {
     
        CreateMap<Data.WellnessRating, Dto.WellnessRating>();

        CreateMap<Dto.WellnessRating, Data.WellnessRating>();

    }
}



