using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dto;
using IServices; 

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
            var dtos = _mapper.Map<Data.Habit[], Dto.Habit[]>(habits);

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
            var _habit = _mapper.Map<HabitForCreation, Data.Habit>(habitDto);

            _habitService.Add(_habit);

            var _habitDto = _mapper.Map<Data.Habit, Dto.Habit>(_habit);

            return CreatedAtAction(nameof(GetAll), new { id = _habitDto.Id }, _habitDto);
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }

    }

    [HttpPut("{id}")]
    public IActionResult Update(Dto.Habit habitDto, Guid id)
    {
        try
        {
            var _habit = _mapper.Map<Dto.Habit, Data.Habit>(habitDto);

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