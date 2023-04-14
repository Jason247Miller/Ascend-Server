using Data;
using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dto;
using IServices;
using Ascend_Server.api.ActionFilters;

namespace Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ServiceFilter(typeof(ModelStateActionFilter))]
[ApiVersion("1.0")]
public class WellnessRatingController : ControllerBase
{
    private readonly IWellnessRatingService _wellnessRatingService;

    private readonly IMapper _mapper;

    public WellnessRatingController(IWellnessRatingService wellnessRatingService,
                                    IMapper mapper)
    {
        _wellnessRatingService = wellnessRatingService;

        _mapper = mapper;
    }
    /// <summary>
    /// Retrieves all wellness ratings for the currently authenticated user.
    /// </summary>
    /// <returns>
    /// Returns an array of <see cref="Dto.WellnessRating"/> objects containing the wellness ratings.
    /// </returns>
    /// <response code="200">Returns the wellness ratings.</response>
    /// <response code="400">The request was invalid.</response>
    /// <response code="404">No wellness ratings were found.</response>
    [HttpGet]
    public IActionResult GetAll()
    {
        //get later from auth service 
        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002");

        try
        {
            var wellnessRatings = _wellnessRatingService.GetAllForUserId(userId);

            if (wellnessRatings == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<Data.WellnessRating[], Dto.WellnessRating[]>(wellnessRatings);

            return new OkObjectResult(dtos);
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
    /// <summary>
    /// Creates a new wellness rating for the currently authenticated user.
    /// </summary>
    /// <param name="wellnessRatingDto">The <see cref="WellnessRatingForCreation"/> object containing the wellness rating data.</param>
    /// <returns>
    /// Returns the newly created <see cref="Dto.WellnessRating"/> object.
    /// </returns>
    /// <response code="201">The wellness rating was successfully created.</response>
    /// <response code="400">The request was invalid.</response>
    [HttpPost]
    public IActionResult Create(WellnessRatingForCreation wellnessRatingDto)
    {
        try
        {
            var _wellnessRating = _mapper.Map<WellnessRatingForCreation, Data.WellnessRating>(wellnessRatingDto);

            _wellnessRatingService.Add(_wellnessRating);

            var _wellnessRatingDto = _mapper.Map<Data.WellnessRating, Dto.WellnessRating>(_wellnessRating);

            return CreatedAtAction(nameof(GetAll), new { id = _wellnessRatingDto.Id }, _wellnessRatingDto);
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
    /// <summary>
    /// Updates a wellness rating for the currently authenticated user.
    /// </summary>
    /// <param name="wellnessRatingDto">The <see cref="Dto.WellnessRating"/> object containing the updated wellness rating data.</param>
    /// <param name="id">The ID of the wellness rating to update.</param>
    /// <response code="204">The wellness rating was successfully updated.</response>
    /// <response code="400">The request was invalid.</response>
    [HttpPut("{id}")]
    public IActionResult Update(Dto.WellnessRating wellnessRatingDto, Guid id)
    {
        try
        {
            var _wellnessRating = _mapper.Map<Dto.WellnessRating, Data.WellnessRating>(wellnessRatingDto);

            _wellnessRatingService.Update(_wellnessRating, id);

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