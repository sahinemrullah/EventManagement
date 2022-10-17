using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Common
{
    public interface IDefinitionEntityCreator<TNamedEntity> where TNamedEntity : IDefinitionEntity
    {
        public IResult<TNamedEntity> FromName(string name);
    }
}