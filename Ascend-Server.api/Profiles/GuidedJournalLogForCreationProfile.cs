using Dto;
using AutoMapper;

namespace profiles;

public class GuidedJournalLogForCreationProfile : Profile
{
    public GuidedJournalLogForCreationProfile()
    {
        CreateMap<GuidedJournalLogForCreation, Data.GuidedJournalLog>()
            .ForMember(
            gjl => gjl.Id,
            options => options.MapFrom(newId => Guid.NewGuid())
            )
            .ForMember(
            gjl => gjl.UserId,
            options => options.MapFrom(src => src.UserId)
            )
            .ForMember(
            gjl => gjl.EntryId,
            options => options.MapFrom(src => src.EntryId)
            )
            .ForMember(
            gjl => gjl.EntryTextValue,
            options => options.MapFrom(src => src.EntryTextValue)
            )
            .ForMember(
            gjl => gjl.Date,
            options => options.MapFrom(src => src.Date)
            );
    }

}