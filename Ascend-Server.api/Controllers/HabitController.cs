using Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ascend_Server.api.Dto;
using Models;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
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
            var dtos = _mapper.Map<Models.Habit[], Ascend_Server.api.Dto.Habit[]>(habits);

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

    [HttpPost]
    public IActionResult Create(HabitForCreation habitDto)
    {
        try
        {
            var _habit = _mapper.Map<HabitForCreation, Models.Habit>(habitDto);

            _habitService.Add(_habit);

            return CreatedAtAction(nameof(GetAll), new { id = habitDto.Id }, habitDto);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }

    }

    [HttpPut("{id}")]
    public IActionResult Update(Ascend_Server.api.Dto.Habit habitDto, Guid id)
    {
        try
        {
            var _habit = _mapper.Map<Ascend_Server.api.Dto.Habit, Models.Habit>(habitDto);

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

}