using Models;
using Services;
using Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class WellnessRatingController : ControllerBase
{
    private readonly IWellnessRatingService _wellnessRatingService;

    public WellnessRatingController(IWellnessRatingService wellnessRatingService)
    {
        _wellnessRatingService = wellnessRatingService;
    }

    [HttpGet("{id}")]
    public ActionResult<List<WellnessRating>> GetAllForUserId(Guid id)
    {
        List<WellnessRating> wellnessRatingsForUserId;

        try
        {
            wellnessRatingsForUserId = _wellnessRatingService.GetAllForUserId(id);
        }
        catch (UserDoesNotExistException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }

        if (wellnessRatingsForUserId == null)
            return NotFound();

        return wellnessRatingsForUserId;
    }

    [HttpPost]
    public IActionResult Create(WellnessRating wellnessRating)
    {
        try
        {
            _wellnessRatingService.Add(wellnessRating);

            return CreatedAtAction(nameof(GetAllForUserId), new { id = wellnessRating.Id }, wellnessRating);
        }
        catch (SameDateException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (UserDoesNotExistException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }

    }


    [HttpPut("{id}")]
    public IActionResult Update(Guid id, WellnessRating wellnessRating)
    {
        try
        {
          //  _wellnessRatingService.Update(wellnessRating);

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