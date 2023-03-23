using Ascend_Server.api.Dto;
using AutoMapper;


namespace profiles;

public class GuidedJournalLogProfile : Profile
{
    public GuidedJournalLogProfile()
    {
        CreateMap<GuidedJournalLog, Models.GuidedJournalLog>()
            .ForMember(
            gjl => gjl.Id,
            options => options.MapFrom(src => src.Id)
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
                
        CreateMap<Models.GuidedJournalLog, Ascend_Server.api.Dto.GuidedJournalLog>();
    }

}