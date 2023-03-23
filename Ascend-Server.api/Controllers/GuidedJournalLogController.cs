using Models;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ascend_Server.api.Dto;

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
            var dtos = _mapper.Map<Models.GuidedJournalLog[], Ascend_Server.api.Dto.GuidedJournalLog[]>(guidedJournalLogs);

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
            var _guidedJournalLog = _mapper.Map<GuidedJournalLogForCreation, Models.GuidedJournalLog>(guidedJournalLogDto);

            _guidedJournalLogService.Add(_guidedJournalLog);

            return CreatedAtAction(nameof(GetAll), new { id = guidedJournalLogDto.Id }, guidedJournalLogDto);
        }
        catch (UserDoesNotExistException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch(SameDateException e)
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
    public IActionResult Update(Ascend_Server.api.Dto.GuidedJournalLog guidedJournalLogDto, Guid id)
    {
        try
        {
            var _guidedJournalLog = _mapper.Map<Ascend_Server.api.Dto.GuidedJournalLog, Models.GuidedJournalLog>(guidedJournalLogDto);

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