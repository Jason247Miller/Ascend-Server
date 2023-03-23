using Ascend_Server.api.Dto;
using AutoMapper;


namespace profiles;

public class GuidedJournalEntry : Profile
{
    public GuidedJournalEntry()
    {
        //map from Dto to Model
        //ForMember specifies how to map a destination member
        //MapFrom Specifies the source to get the data from for the dest member
        //options specifies how to perform the mapping
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
            );

        CreateMap<Models.GuidedJournalEntry, Ascend_Server.api.Dto.GuidedJournalEntry>();
    }

}