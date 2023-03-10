using Models; 
using Services; 
using Exceptions; 
using Microsoft.AspNetCore.Mvc;
 
namespace Controllers; 

[ApiController]
[Route("[controller]")]
public class WellnessRatingController: ControllerBase
{
    public WellnessRatingController()
    {
    //would set the data context here if I had a database
    }
    
    [HttpGet]
    public ActionResult<List<WellnessRating>> GetAll() =>
        WellnessRatingService.GetAll(); 
        
    [HttpGet("{id}")]
    public ActionResult<List<WellnessRating>> GetAllForUserId(int id)
    {
        var wellnessRatingsForUserId = WellnessRatingService.GetAllForUserId(id);

        if(wellnessRatingsForUserId == null)
            return NotFound(); 

        return wellnessRatingsForUserId; 
    }

    [HttpPost]
    public IActionResult Create(WellnessRating wellnessRating)
    {   
        try
        {    Console.WriteLine("Try Post");
            WellnessRatingService.Add(wellnessRating); 
            return CreatedAtAction(nameof(GetAllForUserId), new {id = wellnessRating.Id}, wellnessRating);
        }
        catch(DuplicateWellnessRatingException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch(Exception)
        {
            return new BadRequestResult(); 
        }
        
    }
        
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, WellnessRating wellnessRating)
    {  
        try
        { 
            WellnessRatingService.Update(wellnessRating); 
            
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