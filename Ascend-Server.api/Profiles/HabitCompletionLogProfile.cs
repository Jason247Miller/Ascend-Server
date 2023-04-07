using Dto;
using AutoMapper;

namespace profiles;

public class HabitCompletionLogProfile : Profile
{
    public HabitCompletionLogProfile()
    {
      
        CreateMap<Data.HabitCompletionLog, Dto.HabitCompletionLog>();

        CreateMap<Dto.HabitCompletionLog, Data.HabitCompletionLog>();
    }
}
