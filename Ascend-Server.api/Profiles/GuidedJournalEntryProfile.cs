using Ascend_Server.api.Dto;
using AutoMapper;


namespace profiles;

public class GuidedJournalEntry : Profile
{
    public GuidedJournalEntry()
    {
        CreateMap<Ascend_Server.api.Dto.GuidedJournalEntry, Models.GuidedJournalEntry>()
            .ForMember(
            gje => gje.Id,
            options => options.MapFrom(src => src.Id)
            )
            .ForMember(
            gje => gje.UserId,
            options => options.MapFrom(src => src.UserId)
            )
            .ForMember(
            gje => gje.EntryName,
            options => options.MapFrom(src => src.EntryName)
            )
            .ForMember(
            gje => gje.Deleted,
            options => options.MapFrom(src => src.Deleted)
            )
            .ForMember(
            gje => gje.CreationDate,
            options => options.MapFrom(src => src.CreationDate)
            );

        CreateMap<Models.GuidedJournalEntry, Ascend_Server.api.Dto.GuidedJournalEntry>();
    }

}