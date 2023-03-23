using Ascend_Server.api.Dto;
using AutoMapper;

namespace profiles;

public class HabitForCreationProfile : Profile
{
    public HabitForCreationProfile()
    {
        CreateMap<HabitForCreation, Models.Habit>()
            .ForMember(
            h => h.Id,
            options => options.MapFrom(newId => Guid.NewGuid())
            )
            .ForMember(
            h => h.UserId,
            options => options.MapFrom(src => src.UserId)
            )
            .ForMember(
            h => h.Deleted,
            options => options.MapFrom(src => src.Deleted)
            )
            .ForMember(
            h => h.HabitName,
            options => options.MapFrom(src => src.HabitName)
            )
            .ForMember(
            h => h.HabitName,
            options => options.MapFrom(src => src.HabitName)
            );
    }
}