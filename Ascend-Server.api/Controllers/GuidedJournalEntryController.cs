using Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ascend_Server.api.Dto;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet]
    public IActionResult GetAll()
    {
        // will get from auth service later
        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002");

        try
        {
            var guidedJournalEntries = _guidedJournalEntryService.GetAllForUserId(userId);

            if (guidedJournalEntries == null)
            {
                return NotFound();
            }

            var dtos = _mapper.Map<Models.GuidedJournalEntry[], Ascend_Server.api.Dto.GuidedJournalEntry[]>(guidedJournalEntries);

            return new OkObjectResult(dtos);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }

    }

    [HttpPost]
    public IActionResult Create(GuidedJournalEntryForCreation guidedJournalEntryDto)
    {
        try
        {
            var _guidedJournalEntry = _mapper.Map<GuidedJournalEntryForCreation, Models.GuidedJournalEntry>(guidedJournalEntryDto);

            _guidedJournalEntryService.Add(_guidedJournalEntry);

            var _guidedJournalEntryDto = _mapper.Map<Models.GuidedJournalEntry, Ascend_Server.api.Dto.GuidedJournalEntry>(_guidedJournalEntry);

            return CreatedAtAction(nameof(GetAll), new { id = _guidedJournalEntryDto.Id }, _guidedJournalEntryDto);
        }
        catch (DuplicateEntryException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(GuidedJournalEntry guidedJournalEntryDto, Guid id)
    {
        try
        {
            var _guidedJournalEntry = _mapper.Map<GuidedJournalEntry, Models.GuidedJournalEntry>(guidedJournalEntryDto);

            _guidedJournalEntryService.Update(_guidedJournalEntry, id);

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