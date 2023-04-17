using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dto;
using IServices;
using Ascend_Server.api.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Services;

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

            return new OkObjectResult(dto);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }

    }
    /// <summary>
    /// Gets all the guided journal logs for a user.
    /// </summary>
    /// <response code="200">Returns a list of all the guided journal logs for the user.</response>
    /// <response code="404">If there are no guided journal logs for the user.</response>
    /// <response code="400">If there is an error while retrieving the guided journal logs.</response>
    [HttpGet("userId/{userId}")]
    public IActionResult GetAll(Guid userId)
    {

        try
        {
            var guidedJournalLogs = _guidedJournalLogService.GetAllForUserId(userId);

            if (guidedJournalLogs == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<Data.GuidedJournalLog[], Dto.GuidedJournalLog[]>(guidedJournalLogs);

            return new OkObjectResult(dtos);
        }
        catch (Exception)
        {
            return new BadRequestResult();
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
    public IActionResult Create(GuidedJournalLogForCreation guidedJournalLogDto)
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
            return new BadRequestObjectResult(e.Message);
        }
        catch (SameDateException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (NotFoundException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (System.FormatException e)
        {
            return new BadRequestObjectResult(e.Message);
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
    public IActionResult Update(Dto.GuidedJournalLog guidedJournalLogDto, Guid id)
    {
        try
        {
            var _guidedJournalLog = _mapper.Map<Dto.GuidedJournalLog, Data.GuidedJournalLog>(guidedJournalLogDto);

            _guidedJournalLogService.Update(_guidedJournalLog, id);

            return NoContent();
        }
        catch (NotFoundException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (System.FormatException e)
        {
            return new BadRequestObjectResult(e.Message);
        }

    }

}