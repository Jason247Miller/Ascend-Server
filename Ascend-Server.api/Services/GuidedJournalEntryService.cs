using Models; 
using Exceptions; 
using System;
using Services; 
namespace Services; 

public class GuidedJournalEntryService:IGuidedJournalEntryService
{
     List<GuidedJournalEntry> GuidedJournalEntries { get; }
     int nextId = 3;
     private readonly IUserService _userService; 
     public GuidedJournalEntryService(IUserService userService)
    {
        _userService = userService;
        GuidedJournalEntries = new List<GuidedJournalEntry>
        {
            new GuidedJournalEntry {
                Id = 1,
                UserId = 1,
                EntryName = "What are you most greatful for?",
                Uuid = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec", 
                Deleted = false           
            },

            new GuidedJournalEntry {
                Id = 2,
                UserId = 1,
                EntryName = "What did you learn today?",
                Uuid = "d58a9560-3ed8-4eaa-b97e-c558179861e9", 
                Deleted = false           
            }

        };
    }

    public List<GuidedJournalEntry> GetAllForUserId(int userId)
    {
      if(_userService.CheckUser(userId) == null)
      {
         throw new Exception("Invalid Request");  
      }

      List<GuidedJournalEntry> userEntries = GuidedJournalEntries.Where(gje => gje.UserId == userId).ToList();
      return userEntries; 
    }

    public void Add(GuidedJournalEntry guidedJournalEntry)
    {
         if(_userService.CheckUser(guidedJournalEntry.UserId) == null)
        {
         throw new UserDoesNotExistException();  
        }
        var entriesForUser = GuidedJournalEntries.Where(gje => gje.UserId == guidedJournalEntry.UserId && gje.Uuid == gje.Uuid);
        
        if(entriesForUser.Any())
        {   
            throw new Exception("Bad Request: Unable to Create new Habit due to UUID Duplication");
        }
       
        guidedJournalEntry.Id = nextId++;
        GuidedJournalEntries.Add(guidedJournalEntry);
    }

    public void Update(GuidedJournalEntry guidedJournalEntry)
    {
        var index = GuidedJournalEntries.FindIndex(gje => gje.Id == guidedJournalEntry.Id && gje.UserId == guidedJournalEntry.UserId);
       
        if(index == -1)
        {  
            throw new NotFoundException(guidedJournalEntry); 
        }
        else
        {
            GuidedJournalEntries[index] = guidedJournalEntry;
        }
     
    }
    public bool EntryExistsAndNotDeleted(string? entryIdPassed, int userIdPassed)
    {
       return GuidedJournalEntries.Any(gje => gje.Uuid == entryIdPassed && gje.Deleted == false && gje.UserId == userIdPassed);
        
    }
   
}