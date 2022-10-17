using EventManagement.Application.Features.Cities.DTOs;
using EventManagement.Domain.Collections;
using EventManagement.Domain.Common;
using EventManagement.Domain.Entities;
using EventManagement.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EventManagement.WebAPI.Helpers;
using IResult = EventManagement.Domain.Validations.Interfaces.IResult;
using EventManagement.Application.Features.DefinitionEntities;
using Microsoft.AspNetCore.Authorization;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly IDefinitionEntityService<City> _cityService;
        private readonly IUnitOfWork _unitOfWork;
        public CityController(IDefinitionEntityService<City> cityService, IUnitOfWork unitOfWork)
        {
            _cityService = cityService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromRoute] int id)
        {
            IResult<City> result = _cityService.GetById(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPaginated([FromQuery] PaginatedRequest request)
        {
            IPaginatedList<City> cities = _cityService.GetPaginated(request.PageNumber, request.PageSize);

            if (!cities.Items.Any())
                return NoContent();

            return Ok(cities);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Domain.Entities.Users.User))]
        public IActionResult GetAll()
        {
            IEnumerable<City> cities = _cityService.GetAll();

            if (!cities.Any())
                return NoContent();

            return Ok(cities);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] CityCreateDto createCityDto)
        {
            IResult<City> result = _cityService.Create(createCityDto.Name);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit([FromBody] CityDto cityDto)
        {
            IResult result = _cityService.Update(cityDto.Id, cityDto.Name);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            IResult result = _cityService.Delete(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
