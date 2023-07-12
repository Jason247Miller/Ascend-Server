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
    /// Retrieves a single Habit that matches the passed id.
    /// </summary>
    /// <returns>
    /// Returns a single instance of type <see cref="Dto.Habit"/> object matching the id passed.
    /// </returns>
    /// <response code="200">Returns the Habit.</response>
    /// <response code="400">The request was invalid.</response>
    /// <response code="404">No Habit found for id.</response>
    [HttpGet("id/{id}")]
    public ActionResult<Dto.Habit> GetById(Guid id)
    {
        try
        {
            var habit = _habitService.GetById(id);

            var dto = _mapper.Map<Data.Habit, Dto.Habit>(habit);

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
    /// Retrieves all habits for the user.
    /// </summary>
    /// <returns>A list of habits.</returns>
    /// <response code="200">Returns the list of habits.</response>
    /// <response code="400">If the user does not exist or there is a bad request.</response>
    /// <response code="404">If there are no habits found.</response>
    [HttpGet("userId/{userId}")]
    public ActionResult<IEnumerable<Dto.Habit>> GetAllForUserId(Guid userId)
    {
        try
        {
            var habits = _habitService.GetAllForUserId(userId);

            Console.WriteLine("habits= ", habits.Any());

            if (!habits.Any())
            {
                return NotFound(new { message = "No Habits exists." });
            }

            var dtos = _mapper.Map<IEnumerable<Data.Habit>, IEnumerable<Dto.Habit>>(habits);

            Console.WriteLine("dtos= ", dtos.GetType());

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
    /// Creates a new habit.
    /// </summary>
    /// <param name="habitDto">The habit to create.</param>
    /// <returns>A newly created habit.</returns>
    /// <response code="201">Returns the newly created habit.</response>
    /// <response code="400">If there is a bad request.</response>
    /// <response code="500">If there is an internal server error.</response>
    [HttpPost]
    public ActionResult<Dto.Habit> Create(HabitForCreation habitDto)
    {
        try
        {
            var _habit = _mapper.Map<HabitForCreation, Data.Habit>(habitDto);

            _habitService.Add(_habit);

            var _habitDto = _mapper.Map<Data.Habit, Dto.Habit>(_habit);

            return CreatedAtAction(nameof(GetById), new { id = _habitDto.Id }, _habitDto);
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

    /// <summary>
    /// Updates an existing habit.
    /// </summary>
    /// <param name="habitDto">The habit to update.</param>
    /// <param name="id">The ID of the habit to update.</param>
    /// <returns>A no content response.</returns>
    /// <response code="204">If the habit was updated successfully.</response>
    /// <response code="400">If the habit does not exist or there is a bad request.</response>
    [HttpPut("{id}")]
    public ActionResult Update(Dto.Habit habitDto, Guid id)
    {
        try
        {
            var _habit = _mapper.Map<Dto.Habit, Data.Habit>(habitDto);

            _habitService.Update(_habit, id);

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

    /// <summary>
    /// Deletes an existing habit.
    /// </summary>
    /// <param name="id">The ID of the habit to delete.</param>
    /// <returns>A no content response.</returns>
    /// <response code="204">If the habit was deleted successfully.</response>
    /// <response code="400">If the habit does not exist or there is a bad request.</response>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        try
        {
            _habitService.Delete(id);

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}