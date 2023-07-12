using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dto;
using IServices;
using Ascend_Server.api.ActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace Controllers;

[Authorize]
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
    /// Retrieves a single Wellness Rating that matches the passed id.
    /// </summary>
    /// <returns>
    /// Returns a single instance of type <see cref="Dto.WellnessRating"/> object matching the id passed.
    /// </returns>
    /// <response code="200">Returns the wellness rating.</response>
    /// <response code="400">The request was invalid.</response>
    /// <response code="404">No wellness rating found for id.</response>
    /// 
    [HttpGet("id/{id}")]
    public ActionResult<Dto.WellnessRating> GetById(Guid id)
    {
        try
        {
            var wellnessRating = _wellnessRatingService.GetById(id);

            var dto = _mapper.Map<Data.WellnessRating, Dto.WellnessRating>(wellnessRating);

            return Ok(dto);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

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
    /// 
    [HttpGet("userId/{userId}")]
    public ActionResult<IEnumerable<Dto.WellnessRating>> GetAllForUserId(Guid userId)
    {
        try
        {
            var wellnessRatings = _wellnessRatingService.GetAllForUserId(userId);

            if (!wellnessRatings.Any())
            {
                return NotFound(new { message = "No Wellness Ratings exists." });
            }
            var dtos = _mapper.Map<IEnumerable<Data.WellnessRating>, IEnumerable<Dto.WellnessRating>>(wellnessRatings);

            return Ok(dtos);
        }
        catch (UserDoesNotExistException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
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
    [ActionName(nameof(Create))]
    public ActionResult<Dto.WellnessRating> Create(Dto.WellnessRatingForCreation wellnessRatingDto)
    {
        try
        {
            var _wellnessRating = _mapper.Map<Dto.WellnessRatingForCreation, Data.WellnessRating>(wellnessRatingDto);

            _wellnessRatingService.Add(_wellnessRating);

            var _wellnessRatingDto = _mapper.Map<Data.WellnessRating, Dto.WellnessRating>(_wellnessRating);

            return CreatedAtAction(nameof(GetById), new { id = _wellnessRatingDto.Id }, _wellnessRatingDto);
        }
        catch (SameDateException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (System.FormatException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
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
    public ActionResult<IEnumerable<Dto.WellnessRating>> Update(Dto.WellnessRating wellnessRatingDto, Guid id)
    {
        try
        {
            var _wellnessRating = _mapper.Map<Dto.WellnessRating, Data.WellnessRating>(wellnessRatingDto);

            _wellnessRatingService.Update(_wellnessRating, id);

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (System.FormatException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

}