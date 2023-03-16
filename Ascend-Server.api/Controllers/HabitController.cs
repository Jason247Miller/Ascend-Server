using Models; 
using Services; 
using Exceptions; 
using Microsoft.AspNetCore.Mvc;

namespace Controllers; 

[ApiController]
[Route("api/[controller]")]
public class HabitController: ControllerBase
{ 
    private readonly IHabitService _habitService;

    public HabitController(IHabitService habitService)
    {
     _habitService = habitService; 
    }
    [HttpGet("{id}")]
    public ActionResult<List<Habit>> GetAllForUserId(int id)
    {   List<Habit> habitsForUserId;
        try
        {
            habitsForUserId = _habitService.GetAllForUserId(id); 
        }
        
        catch(Exception e)
        {
            return new BadRequestObjectResult(e.Message); 
        }

        if(habitsForUserId == null)
            return NotFound(); 

      return habitsForUserId; 
    }

    

    [HttpPost]
    public IActionResult Create(Habit habit)
    { 
     try
        {   
            _habitService.Add(habit); 
            return CreatedAtAction(nameof(GetAllForUserId), new {id = habit.Id}, habit);
        }
        catch(Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        
       
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Habit habit)
    {  
        try
        { 
            _habitService.Update(habit); 
            
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