using EventManagement.Domain.Collections;
using EventManagement.Domain.Common;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Application.Features.DefinitionEntities
{
    public interface IDefinitionEntityService<TDefinitionEntity>
        where TDefinitionEntity : IDefinitionEntity
    {
        public IEnumerable<TDefinitionEntity> GetAll();
        public IPaginatedList<TDefinitionEntity> GetPaginated(int pageNumber, int pageSize);
        public IResult<TDefinitionEntity> GetById(int id);
        public IResult<TDefinitionEntity> Create(string name);
        public IResult Update(int id, string name);
        public IResult Delete(int id);
    }
}
