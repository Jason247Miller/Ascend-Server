using Models; 
using Exceptions; 
using Microsoft.AspNetCore.Mvc;
namespace Controllers; 

[ApiController]
[Route("api/[controller]")]
public class HabitCompletionLogController : ControllerBase
{
    private readonly IHabitCompletionLogService _habitCompletionLogService;

    public HabitCompletionLogController(IHabitCompletionLogService habitCompletionLogService)
    {
        _habitCompletionLogService = habitCompletionLogService;
    }

[HttpGet("{id}")]
public ActionResult<List<HabitCompletionLog>> GetAllForUserId(int id)
{   
    List<HabitCompletionLog> habitCompletionLogsForUserId;
    try
    {
        habitCompletionLogsForUserId = _habitCompletionLogService.GetAllForUserId(id); 
    }
    catch(Exception)
    {
        return new BadRequestResult(); 
    }

    if(habitCompletionLogsForUserId == null)
        return NotFound(); 

    return habitCompletionLogsForUserId; 
}

[HttpPost]
public IActionResult Create(HabitCompletionLog habitCompletionLog)
{ 
    try
    {   
    _habitCompletionLogService.Add(habitCompletionLog); 

        return CreatedAtAction(nameof(GetAllForUserId), new {id = habitCompletionLog.Id}, habitCompletionLog);
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
    public IActionResult Update(int id, HabitCompletionLog habitCompletionLog)
    {  
        try
        { 
            _habitCompletionLogService.Update(habitCompletionLog); 
            
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