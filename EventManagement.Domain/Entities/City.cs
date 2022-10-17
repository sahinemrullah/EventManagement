using EventManagement.Domain.Common;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Entities
{
    public sealed class City : IDefinitionEntity
    {
        internal City() { }
        public City(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; } = null!;

        public IResult SetName(string name)
        {
            ValidationResult validationResult = new();

            validationResult.ValidateString("Name", name, 30);

            if (validationResult.IsSuccess)
                Name = name;

            return validationResult;
        }
    }
    internal sealed class CityCreator : IDefinitionEntityCreator<City>
    {
        public IResult<City> FromName(string name)
        {
            City city = new();
            IResult result = city.SetName(name);

            if (!result.IsSuccess)
                return Result<City>.Failure(result.Exception!);

            return Result<City>.Success(city);
        }
    }
}