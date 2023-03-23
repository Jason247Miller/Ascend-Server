using Ascend_Server.api.Dto;
using AutoMapper;

namespace profiles;

public class HabitProfile : Profile
{
    public HabitProfile()
    {
        CreateMap<Habit, Models.Habit>()
            .ForMember(
            h => h.Id,
            options => options.MapFrom(src => src.Id)
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
        CreateMap<Models.Habit, Ascend_Server.api.Dto.Habit>();
    }
}