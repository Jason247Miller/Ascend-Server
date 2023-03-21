using Models;
using Services;
using Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuidedJournalEntriesController : ControllerBase
{
    private readonly IGuidedJournalEntryService _guidedJournalEntryService;

    public GuidedJournalEntriesController(IGuidedJournalEntryService guidedJournalEntryService)
    {
        _guidedJournalEntryService = guidedJournalEntryService;
    }

    [HttpGet]
    public ActionResult<List<GuidedJournalEntry>> GetAllGuidedJournalEntries()
    {
        List<GuidedJournalEntry> guidedJournalEntries;

        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"); //will get from auth service later

        try
        {
            guidedJournalEntries = _guidedJournalEntryService.GetAll(userId);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }

        if (guidedJournalEntries == null)
            return new List<GuidedJournalEntry>();

        return guidedJournalEntries;
    }

    [HttpPost]
    public IActionResult Create(GuidedJournalEntry guidedJournalEntry)
    {
        try
        {
            _guidedJournalEntryService.Add(guidedJournalEntry);

            return CreatedAtAction(nameof(GetAllGuidedJournalEntries), new { id = guidedJournalEntry.Id }, guidedJournalEntry);
        }
        catch (DuplicateEntryException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, GuidedJournalEntry guidedJournalEntry)
    {
        try
        {
            _guidedJournalEntryService.Update(guidedJournalEntry);

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

}