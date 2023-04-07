using Data;
using Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dto;
using IServices;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class WellnessRatingController : ControllerBase
{
    private readonly IWellnessRatingService _wellnessRatingService;

    private readonly IMapper _mapper;

    public WellnessRatingController(IWellnessRatingService wellnessRatingService,
                                    IMapper mapper)
    {
        _wellnessRatingService = wellnessRatingService;

        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        //get later from auth service 
        Guid userId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002");

        try
        {
            var wellnessRatings = _wellnessRatingService.GetAllForUserId(userId);

            if (wellnessRatings == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<Data.WellnessRating[], Dto.WellnessRating[]>(wellnessRatings);

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
    public IActionResult Create(WellnessRatingForCreation wellnessRatingDto)
    {
        try
        {
            var _wellnessRating = _mapper.Map<WellnessRatingForCreation, Data.WellnessRating>(wellnessRatingDto);

            _wellnessRatingService.Add(_wellnessRating);

            var _wellnessRatingDto = _mapper.Map<Data.WellnessRating,Dto.WellnessRating>(_wellnessRating);

            return CreatedAtAction(nameof(GetAll), new { id = _wellnessRatingDto.Id }, _wellnessRatingDto);
        }
        catch (SameDateException e)
        {
            return new BadRequestObjectResult(e.Message);
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

    [HttpPut("{id}")]
    public IActionResult Update(Dto.WellnessRating wellnessRatingDto, Guid id)
    {
        try
        {
            var _wellnessRating = _mapper.Map<Dto.WellnessRating, Data.WellnessRating>(wellnessRatingDto);

            _wellnessRatingService.Update(_wellnessRating, id);

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