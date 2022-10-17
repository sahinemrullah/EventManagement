using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Domain.Common
{
    public interface IDefinitionEntity : IEntity
    {
        public string Name { get; }
        public IResult SetName(string name);
    }
}