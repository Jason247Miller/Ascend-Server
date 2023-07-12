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
public class GuidedJournalLogController : ControllerBase
{
    private readonly IGuidedJournalLogService _guidedJournalLogService;

    private readonly IMapper _mapper;

    public GuidedJournalLogController(IGuidedJournalLogService guidedJournalLogService,
                                      IMapper mapper)
    {
        _guidedJournalLogService = guidedJournalLogService;

        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a single Guided Journal Log that matches the passed id.
    /// </summary>
    /// <returns>
    /// Returns a single instance of type <see cref="Dto.GuidedJournalLog"/> object matching the id passed.
    /// </returns>
    /// <response code="200">Returns the Guided Journal Log.</response>
    /// <response code="400">The request was invalid.</response>
    /// <response code="404">No guided journal Log found for id.</response>
    /// 
    [HttpGet("id/{id}")]
    public ActionResult<Dto.GuidedJournalLog> GetById(Guid id)
    {
        try
        {
            var guidedJournalLog = _guidedJournalLogService.GetById(id);

            var dto = _mapper.Map<Data.GuidedJournalLog, Dto.GuidedJournalLog>(guidedJournalLog);

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
    /// Gets all the guided journal logs for a user.
    /// </summary>
    /// <response code="200">Returns a list of all the guided journal logs for the user.</response>
    /// <response code="404">If there are no guided journal logs for the user.</response>
    /// <response code="400">If there is an error while retrieving the guided journal logs.</response>
    [HttpGet("userId/{userId}")]
    public ActionResult<IEnumerable<Dto.GuidedJournalLog>> GetAllForUserId(Guid userId)
    {
        try
        {
            var guidedJournalLogs = _guidedJournalLogService.GetAllForUserId(userId);

            if (!guidedJournalLogs.Any())
            {
                return NotFound(new { message = "No Guided Journal Logs exists." });
            }
            var dtos = _mapper.Map<IEnumerable<Data.GuidedJournalLog>, IEnumerable<Dto.GuidedJournalLog>>(guidedJournalLogs);

            return Ok(dtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Creates a new guided journal log for the currently authenticated user.
    /// </summary>
    /// <param name="guidedJournalLogDto">The DTO containing the guided journal log data to create.</param>
    /// <returns>The newly created guided journal log.</returns>
    /// <response code="201">Returns the newly created guided journal log.</response>
    /// <response code="400">If the guided journal log data is invalid or the creation was unsuccessful.</response>
    [HttpPost]
    public ActionResult Create(GuidedJournalLogForCreation guidedJournalLogDto)
    {
        try
        {
            var _guidedJournalLog = _mapper.Map<GuidedJournalLogForCreation, Data.GuidedJournalLog>(guidedJournalLogDto);

            _guidedJournalLogService.Add(_guidedJournalLog);

            var _guidedJournalLogDto = _mapper.Map<Data.GuidedJournalLog, Dto.GuidedJournalLog>(_guidedJournalLog);

            return CreatedAtAction(nameof(GetById), new { id = _guidedJournalLogDto.Id }, _guidedJournalLogDto);
        }
        catch (UserDoesNotExistException e)
        {
            return BadRequest(e.Message);
        }
        catch (SameDateException e)
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
    /// Updates a guided journal log by ID.
    /// </summary>
    /// <param name="guidedJournalLogDto">The DTO containing the updated data for the guided journal log.</param>
    /// <param name="id">The ID of the guided journal log to update.</param>
    /// <response code="204">Indicates the guided journal log was successfully updated.</response>
    /// <response code="400">Indicates a bad request was received or the guided journal log was not found.</response>
    [HttpPut("{id}")]
    public ActionResult Update(Dto.GuidedJournalLog guidedJournalLogDto, Guid id)
    {
        try
        {
            var _guidedJournalLog = _mapper.Map<Dto.GuidedJournalLog, Data.GuidedJournalLog>(guidedJournalLogDto);

            _guidedJournalLogService.Update(_guidedJournalLog, id);

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