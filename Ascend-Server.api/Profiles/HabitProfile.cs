using Dto;
using AutoMapper;


namespace profiles;

public class HabitProfile : Profile
{
    public HabitProfile()
    {
        CreateMap<Data.Habit, Dto.Habit>();

        CreateMap<Dto.Habit, Data.Habit>();
    }
}