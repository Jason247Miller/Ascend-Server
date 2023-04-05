using Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ascend_Server.api.Dto;
using Models;
using Ascend_Server.api.IConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public HabitController(IUnitOfWork unitOfWork,
                           IMapper mapper)
    {
        _unitOfWork = unitOfWork;

        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //will get from auth service later
        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002");

        try
        {
            var habits = await _unitOfWork.Habits.GetAllForUserId(userId);

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
    public async Task<IActionResult> Create(HabitForCreation habitDto)
    {
        try
        {
            var _habit = _mapper.Map<HabitForCreation, Models.Habit>(habitDto);

            await _unitOfWork.Habits.Add(_habit);

            await _unitOfWork.CompleteAsync();

            var _habitDto = _mapper.Map<Models.Habit, Ascend_Server.api.Dto.Habit>(_habit);

            return CreatedAtAction(nameof(GetAll), new { id = _habitDto.Id }, _habitDto);
        }
        catch (DuplicateHabitException e)
        {
            return Conflict(e.Message);
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

            _unitOfWork.Habits.Update(_habit, id);

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