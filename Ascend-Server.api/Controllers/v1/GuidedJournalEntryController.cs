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
public class GuidedJournalEntryController : ControllerBase
{
    private readonly IGuidedJournalEntryService _guidedJournalEntryService;

    private readonly IMapper _mapper;

    public GuidedJournalEntryController(IGuidedJournalEntryService guidedJournalEntryService,
                                        IMapper mapper)
    {
        _guidedJournalEntryService = guidedJournalEntryService;

        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a single Guided Journal Entry that matches the passed id.
    /// </summary>
    /// <returns>
    /// Returns a single instance of type <see cref="Dto.GuidedJournalEntry"/> object matching the id passed.
    /// </returns>
    /// <response code="200">Returns the Guided Journal Entry.</response>
    /// <response code="400">The request was invalid.</response>
    /// <response code="404">No guided journal entry found for id.</response>
    /// 
    [HttpGet("id/{id}")]
    public ActionResult<Dto.GuidedJournalEntry> GetById(Guid id)
    {
        try
        {
            var guidedJournalEntry = _guidedJournalEntryService.GetById(id);

            var dto = _mapper.Map<Data.GuidedJournalEntry, Dto.GuidedJournalEntry>(guidedJournalEntry);

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
    /// Gets all guided journal entries for the authenticated user.
    /// </summary>
    /// <returns>A list of GuidedJournalEntry DTOs if successful, or a NotFoundResult if no entries were found.</returns>
    /// <response code="200">Returns a list of GuidedJournalEntry DTOs if successful.</response>
    /// <response code="404">If no entries were found for the authenticated user.</response>
    [HttpGet("userId/{userId}")]
    public ActionResult GetAllForUserId(Guid userId)
    {
        try
        {
            var guidedJournalEntries = _guidedJournalEntryService.GetAllForUserId(userId);

            if (!guidedJournalEntries.Any())
            {
                return NotFound(new { message = "No Guided Journal Entries exists." });
            }

            var dtos = _mapper.Map<IEnumerable<Data.GuidedJournalEntry>, IEnumerable<Dto.GuidedJournalEntry>>(guidedJournalEntries);

            return Ok(dtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    /// <summary>
    /// Creates a new GuidedJournalEntry based on the provided DTO.
    /// </summary>
    /// <param name="guidedJournalEntryDto">The GuidedJournalEntryForCreation DTO containing the data for the new entry.</param>
    /// <returns>A CreatedAtActionResult if successful, or a BadRequestResult if an error occurred.</returns>
    /// <response code="201">Returns the created GuidedJournalEntry DTO if successful.</response>
    /// <response code="400">If an error occurred while creating the new entry.</response>
    [HttpPost]
    public ActionResult Create(GuidedJournalEntryForCreation guidedJournalEntryDto)
    {
        try
        {
            var _guidedJournalEntry = _mapper.Map<GuidedJournalEntryForCreation, Data.GuidedJournalEntry>(guidedJournalEntryDto);

            _guidedJournalEntryService.Add(_guidedJournalEntry);

            var _guidedJournalEntryDto = _mapper.Map<Data.GuidedJournalEntry, Dto.GuidedJournalEntry>(_guidedJournalEntry);

            return CreatedAtAction(nameof(GetById), new { id = _guidedJournalEntryDto.Id }, _guidedJournalEntryDto);
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
    /// Updates the GuidedJournalEntry with the provided ID based on the provided DTO.
    /// </summary>
    /// <param name="guidedJournalEntryDto">The GuidedJournalEntry DTO containing the updated data for the entry.</param>
    /// <param name="id">The ID of the entry to update.</param>
    /// <returns>A NoContentResult if successful, or a BadRequestResult if an error occurred.</returns>
    /// <response code="204">If the entry was updated successfully.</response>
    /// <response code="400">If an error occurred while updating the entry.</response>
    [HttpPut("{id}")]
    public ActionResult Update(GuidedJournalEntry guidedJournalEntryDto, Guid id)
    {
        try
        {
            var _guidedJournalEntry = _mapper.Map<GuidedJournalEntry, Data.GuidedJournalEntry>(guidedJournalEntryDto);

            _guidedJournalEntryService.Update(_guidedJournalEntry, id);

            return NoContent();
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
    /// Deletes a GuidedJournalEntry for the given id.
    /// </summary>
    /// <param name="id">The id of the GuidedJournalEntry to delete.</param>
    /// <response code="204">The GuidedJournalEntry was deleted successfully.</response>
    /// <response code="400">The request was invalid or the GuidedJournalEntry was not found.</response>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        try
        {
            _guidedJournalEntryService.Delete(id);

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