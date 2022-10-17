using EventManagement.Domain.Common;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Entities
{
    public sealed class Category : IDefinitionEntity
    {
        public Category(string name)
        {
            SetName(name);
        }
        internal Category() { }

        public int Id { get; private set; }
        public string Name { get; private set; } = null!;

        public IResult SetName(string name)
        {
            ValidationResult validationResult = new();

            validationResult.ValidateString("Name", name, 30);

            if(validationResult.IsSuccess)
                Name = name;

            return validationResult;
        }
    }
    internal sealed class CategoryCreator : IDefinitionEntityCreator<Category>
    {
        public IResult<Category> FromName(string name)
        {
            Category category = new();
            IResult result = category.SetName(name);

            if (!result.IsSuccess)
                return Result<Category>.Failure(result.Exception!);

            return Result<Category>.Success(category);
        }
    }
}