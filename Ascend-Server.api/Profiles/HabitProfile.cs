using Ascend_Server.api.Dto;
using AutoMapper;
using Models;

namespace profiles;

public class HabitProfile : Profile
{
    public HabitProfile()
    {
        CreateMap<Ascend_Server.api.Dto.Habit, Models.Habit>()
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
            h => h.CreationDate,
            options => options.MapFrom(src => src.CreationDate)
            );
        CreateMap<Models.Habit, Ascend_Server.api.Dto.Habit>();
    }
}