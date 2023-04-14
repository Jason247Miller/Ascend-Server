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
    /// GET endpoint to retrieve all habit completion logs for a specific user.
    /// </summary>
    /// <returns>Returns a list of habit completion logs for the user or returns NotFound if there are none.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        // will get from auth service later
        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002");

        try
        {
            var habitCompletionLogs = _habitCompletionLogService.GetAllForUserId(userId);

            if (habitCompletionLogs == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<Data.HabitCompletionLog[], Dto.HabitCompletionLog[]>(habitCompletionLogs);

            return new OkObjectResult(dtos);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }
    /// <summary>
    /// POST endpoint to create a new habit completion log.
    /// </summary>
    /// <param name="habitCompletionLogDto">The habit completion log DTO to create.</param>
    /// <returns>Returns CreatedAtAction with the created habit completion log DTO or BadRequestObjectResult with the error message if unsuccessful.</returns>
    [HttpPost]
    public IActionResult Create(Dto.HabitCompletionLogForCreation habitCompletionLogDto)
    {
        try
        {
            var _habitCompletionLog = _mapper.Map<HabitCompletionLogForCreation, Data.HabitCompletionLog>(habitCompletionLogDto);

            _habitCompletionLogService.Add(_habitCompletionLog);

            var _habitCompletionLogDto = _mapper.Map<Data.HabitCompletionLog, Dto.HabitCompletionLog>(_habitCompletionLog);

            return CreatedAtAction(nameof(GetAll), new { id = _habitCompletionLogDto.Id }, _habitCompletionLogDto);
        }
        catch (SameDateException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (UserDoesNotExistException e)
        {
            return new BadRequestObjectResult(e.Message);
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
    /// PUT endpoint to update an existing habit completion log.
    /// </summary>
    /// <param name="habitCompletionLogDto">The updated habit completion log DTO.</param>
    /// <param name="id">The id of the habit completion log to update.</param>
    /// <returns>Returns NoContent if successful or BadRequestObjectResult with the error message if unsuccessful.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(Dto.HabitCompletionLog habitCompletionLogDto, Guid id)
    {
        try
        {
            var _habitCompletionLog = _mapper.Map<Dto.HabitCompletionLog, Data.HabitCompletionLog>(habitCompletionLogDto);

            _habitCompletionLogService.Update(_habitCompletionLog, id);

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