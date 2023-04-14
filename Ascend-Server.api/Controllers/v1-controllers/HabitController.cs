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
public class HabitController : ControllerBase
{
    private readonly IHabitService _habitService;

    private readonly IMapper _mapper;

    public HabitController(IHabitService habitService,
                           IMapper mapper)
    {
        _habitService = habitService;

        _mapper = mapper;
    }
    /// <summary>
    /// Retrieves all habits for the user.
    /// </summary>
    /// <returns>A list of habits.</returns>
    /// <response code="200">Returns the list of habits.</response>
    /// <response code="400">If the user does not exist or there is a bad request.</response>
    /// <response code="404">If there are no habits found.</response>
    [HttpGet]
    public IActionResult GetAll()
    {
        // will get from auth service later
        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002");

        try
        {
            var habits = _habitService.GetAllForUserId(userId);

            if (habits == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<Data.Habit[], Dto.Habit[]>(habits);

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
    /// Creates a new habit.
    /// </summary>
    /// <param name="habitDto">The habit to create.</param>
    /// <returns>A newly created habit.</returns>
    /// <response code="201">Returns the newly created habit.</response>
    /// <response code="400">If there is a bad request.</response>
    /// <response code="500">If there is an internal server error.</response>
    [HttpPost]
    public IActionResult Create(HabitForCreation habitDto)
    {
        try
        {
            var _habit = _mapper.Map<HabitForCreation, Data.Habit>(habitDto);

            _habitService.Add(_habit);

            var _habitDto = _mapper.Map<Data.Habit, Dto.Habit>(_habit);

            return CreatedAtAction(nameof(GetAll), new { id = _habitDto.Id }, _habitDto);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }

    }
    /// <summary>
    /// Updates an existing habit.
    /// </summary>
    /// <param name="habitDto">The habit to update.</param>
    /// <param name="id">The ID of the habit to update.</param>
    /// <returns>A no content response.</returns>
    /// <response code="204">If the habit was updated successfully.</response>
    /// <response code="400">If the habit does not exist or there is a bad request.</response>
    [HttpPut("{id}")]
    public IActionResult Update(Dto.Habit habitDto, Guid id)
    {
        try
        {
            var _habit = _mapper.Map<Dto.Habit, Data.Habit>(habitDto);

            _habitService.Update(_habit, id);

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
    /// <summary>
    /// Deletes an existing habit.
    /// </summary>
    /// <param name="habitDto">The habit to delete.</param>
    /// <param name="id">The ID of the habit to delete.</param>
    /// <returns>A no content response.</returns>
    /// <response code="204">If the habit was deleted successfully.</response>
    /// <response code="400">If the habit does not exist or there is a bad request.</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(Dto.Habit habitDto, Guid id)
    {
        try
        {
            var _habit = _mapper.Map<Dto.Habit, Data.Habit>(habitDto);

            _habitService.Delete(_habit, id);

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