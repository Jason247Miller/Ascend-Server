using AutoMapper;

namespace profiles;

public class GuidedJournalEntry : Profile
{
    public GuidedJournalEntry()
    {         
        CreateMap<Dto.GuidedJournalEntry, Data.GuidedJournalEntry>();

        CreateMap<Data.GuidedJournalEntry, Dto.GuidedJournalEntry>();
    }

}