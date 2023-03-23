using Ascend_Server.api.Dto;
using AutoMapper;

namespace profiles;

public class HabitCompletionLogProfile : Profile
{
    public HabitCompletionLogProfile()
    {
        CreateMap<HabitCompletionLog, Models.HabitCompletionLog>()
            .ForMember(
            hcl => hcl.Id,
            options => options.MapFrom(src => src.Id)
            )
            .ForMember(
            hcl => hcl.UserId,
            options => options.MapFrom(src => src.UserId)
            )
            .ForMember(
            hcl => hcl.HabitId,
            options => options.MapFrom(src => src.HabitId)
            )
            .ForMember(
            hcl => hcl.Completed,
            options => options.MapFrom(src => src.Completed)
            )
            .ForMember(
            hcl => hcl.Date,
            options => options.MapFrom(src => src.Date)
            );
        CreateMap<Models.HabitCompletionLog, Ascend_Server.api.Dto.HabitCompletionLog>();
    }
}
