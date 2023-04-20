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
public class HabitCompletionLogController : ControllerBase
{
    private readonly IHabitCompletionLogService _habitCompletionLogService;

    private readonly IMapper _mapper;
    public HabitCompletionLogController(IHabitCompletionLogService habitCompletionLogService,
                                        IMapper mapper)
    {
        _habitCompletionLogService = habitCompletionLogService;

        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a single Habit Completion Log that matches the passed id.
    /// </summary>
    /// <returns>
    /// Returns a single instance of type <see cref="Dto.HabitCompletionLog"/> object matching the id passed.
    /// </returns>
    /// <response code="200">Returns the Habit Completion Log.</response>
    /// <response code="400">The request was invalid.</response>
    /// <response code="404">No Habit Completion Log found for id.</response>
    [HttpGet("id/{id}")]
    public ActionResult<Dto.HabitCompletionLog> GetById(Guid id)
    {
        try
        {
            var habitCompletionLog = _habitCompletionLogService.GetById(id);

            var dto = _mapper.Map<Data.HabitCompletionLog, Dto.HabitCompletionLog>(habitCompletionLog);

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
    /// GET endpoint to retrieve all habit completion logs for a specific user.
    /// </summary>
    /// <returns>Returns a list of habit completion logs for the user or returns NotFound if there are none.</returns>
    [HttpGet("userId/{userId}")]
    public ActionResult<IEnumerable<HabitCompletionLog>> GetAllForUserId(Guid userId)
    {
        try
        {
            var habitCompletionLogs = _habitCompletionLogService.GetAllForUserId(userId);

            if (!habitCompletionLogs.Any())
            {
                return NotFound(new { message = "No Habit Logs exists." });
            }
            var dtos = _mapper.Map<IEnumerable<Data.HabitCompletionLog>, IEnumerable<Dto.HabitCompletionLog>>(habitCompletionLogs);

            return Ok(dtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    /// <summary>
    /// POST endpoint to create a new habit completion log.
    /// </summary>
    /// <param name="habitCompletionLogDto">The habit completion log DTO to create.</param>
    /// <returns>Returns CreatedAtAction with the created habit completion log DTO or BadRequestObjectResult with the error message if unsuccessful.</returns>
    [HttpPost]
    public ActionResult Create(Dto.HabitCompletionLogForCreation habitCompletionLogDto)
    {
        try
        {
            var _habitCompletionLog = _mapper.Map<HabitCompletionLogForCreation, Data.HabitCompletionLog>(habitCompletionLogDto);

            _habitCompletionLogService.Add(_habitCompletionLog);

            var _habitCompletionLogDto = _mapper.Map<Data.HabitCompletionLog, Dto.HabitCompletionLog>(_habitCompletionLog);
            Console.WriteLine("Habit has been Added"); 
            return CreatedAtAction(nameof(GetById), new { id = _habitCompletionLogDto.Id }, _habitCompletionLogDto);
        }
        catch (SameDateException e)
        {
            return BadRequest(e.Message);
        }
        catch (UserDoesNotExistException e)
        {
            return BadRequest(e.Message);
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
    /// PUT endpoint to update an existing habit completion log.
    /// </summary>
    /// <param name="habitCompletionLogDto">The updated habit completion log DTO.</param>
    /// <param name="id">The id of the habit completion log to update.</param>
    /// <returns>Returns NoContent if successful or BadRequestObjectResult with the error message if unsuccessful.</returns>
    [HttpPut("{id}")]
    public ActionResult Update(Dto.HabitCompletionLog habitCompletionLogDto, Guid id)
    {
        try
        {

            var _habitCompletionLog = _mapper.Map<Dto.HabitCompletionLog, Data.HabitCompletionLog>(habitCompletionLogDto);
           

            _habitCompletionLogService.Update(_habitCompletionLog, id);
            Console.WriteLine("habit updated successfully");
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