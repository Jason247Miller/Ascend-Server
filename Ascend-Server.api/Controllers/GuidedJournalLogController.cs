using Models; 
using Exceptions; 
using Microsoft.AspNetCore.Mvc;

namespace Controllers; 

[ApiController]
[Route("api/[controller]")]
public class GuidedJournalLogController : ControllerBase
{
    private readonly IGuidedJournalLogService _guidedJournalLogService;

    public GuidedJournalLogController(IGuidedJournalLogService guidedJournalLogService)
    {
        _guidedJournalLogService = guidedJournalLogService;
    }

[HttpGet("{id}")]
public ActionResult<List<GuidedJournalLog>> GetAllForUserId(int id)
{   
    List<GuidedJournalLog> guidedJournalLogsForUserId;
    try
    {
        guidedJournalLogsForUserId = _guidedJournalLogService.GetAllForUserId(id); 
    }
    catch(Exception e)
    {
        return new BadRequestObjectResult(e.Message); 
    }

    if(guidedJournalLogsForUserId == null)
        return NotFound(); 

    return guidedJournalLogsForUserId; 
}

[HttpPost]
public IActionResult Create(GuidedJournalLog guidedJournalLog)
{ 
    try
    {   
    _guidedJournalLogService.Add(guidedJournalLog); 

        return CreatedAtAction(nameof(GetAllForUserId), new {id = guidedJournalLog.Id}, guidedJournalLog);
    }
    catch(UserDoesNotExistException e)
    {
        return new BadRequestObjectResult(e.Message);
    }
    catch(HabitNotFoundException e)
    {
       return new BadRequestObjectResult(e.Message);
    }
    catch(Exception)
    {
       return new BadRequestResult();
    }
}

[HttpPut("{id}")]
    public IActionResult Update(int id, GuidedJournalLog guidedJournalLog)
    {  
        try
        { 
            _guidedJournalLogService.Update(guidedJournalLog); 
            
            return NoContent(); 
        }
        catch(NotFoundException e )
        {  
            return new BadRequestObjectResult(e.Message);
        }
        catch(Exception)
        {
            return new BadRequestResult(); 
        }
    }

}