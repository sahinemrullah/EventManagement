using EventManagement.Application.Features.Categories.DTOs;
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
    public class CategoryController : ControllerBase
    {
        private readonly IDefinitionEntityService<Category> _categoryService;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IDefinitionEntityService<Category> categoryService, IUnitOfWork unitOfWork)
        {
            _categoryService = categoryService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromRoute] int id)
        {
            IResult<Category> result = _categoryService.GetById(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPaginated([FromQuery] PaginatedRequest request)
        {
            IPaginatedList<Category> categories = _categoryService.GetPaginated(request.PageNumber, request.PageSize);

            if (!categories.Items.Any())
                return NoContent();

            return Ok(categories);
        }

        [HttpGet]
        [Authorize(Roles = $"Admin,{nameof(Domain.Entities.Users.User)}")]
        public IActionResult GetAll()
        {
            IEnumerable<Category> categories = _categoryService.GetAll();

            if (!categories.Any())
                return NoContent();

            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] CategoryCreateDto createCategoryDto)
        {
            IResult<Category> result = _categoryService.Create(createCategoryDto.Name);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit([FromBody] CategoryDto categoryDto)
        {
            IResult result = _categoryService.Update(categoryDto.Id, categoryDto.Name);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            IResult result = _categoryService.Delete(id);

            if (!result.IsSuccess)
                return result.GetExceptionStatusResult();

            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
