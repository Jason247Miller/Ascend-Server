﻿using Dto;
using AutoMapper;

namespace profiles;

public class HabitCompletionLogForCreationProfile : Profile
{
    public HabitCompletionLogForCreationProfile()
    {
        CreateMap<HabitCompletionLogForCreation, Data.HabitCompletionLog>()
            .ForMember(
            hcl => hcl.Id,
            options => options.MapFrom(newId => Guid.NewGuid())
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
    }
}
