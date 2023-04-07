using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dto;
using IServices;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet]
    public IActionResult GetAll()
    {

        // will get from auth service later
        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002");

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

    [HttpPost]
    public IActionResult Create(GuidedJournalLogForCreation guidedJournalLogDto)
    {
        try
        {
            var _guidedJournalLog = _mapper.Map<GuidedJournalLogForCreation, Data.GuidedJournalLog>(guidedJournalLogDto);

            _guidedJournalLogService.Add(_guidedJournalLog);

            var _guidedJournalLogDto = _mapper.Map<Data.GuidedJournalLog, Dto.GuidedJournalLog>(_guidedJournalLog);

            return CreatedAtAction(nameof(GetAll), new { id = guidedJournalLogDto.Id }, guidedJournalLogDto);
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
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

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
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }

}