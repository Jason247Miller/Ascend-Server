using Models;
using Services;
using Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuidedJournalEntryController : ControllerBase
{
    private readonly IGuidedJournalEntryService _guidedJournalEntryService;

    public GuidedJournalEntryController(IGuidedJournalEntryService guidedJournalEntryService)
    {
        _guidedJournalEntryService = guidedJournalEntryService;
    }

    [HttpGet("{id}")]
    public ActionResult<List<GuidedJournalEntry>> GetAllForUserId(int id)
    {
        List<GuidedJournalEntry> entriesForUserId;

        try
        {
            entriesForUserId = _guidedJournalEntryService.GetAllForUserId(id);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }

        if (entriesForUserId == null)
            return NotFound();

        return entriesForUserId;
    }

    [HttpPost]
    public IActionResult Create(GuidedJournalEntry guidedJournalEntry)
    {
        try
        {
            _guidedJournalEntryService.Add(guidedJournalEntry);

            return CreatedAtAction(nameof(GetAllForUserId), new { id = guidedJournalEntry.Id }, guidedJournalEntry);
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
    public IActionResult Update(int id, GuidedJournalEntry guidedJournalEntry)
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