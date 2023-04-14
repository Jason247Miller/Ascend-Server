using Dto;
using AutoMapper;

namespace profiles;

public class GuidedJournalLogProfile : Profile
{
    public GuidedJournalLogProfile()
    {      

        CreateMap<Data.GuidedJournalLog, Dto.GuidedJournalLog>();

        CreateMap<Dto.GuidedJournalLog, Data.GuidedJournalLog>();
    }

}