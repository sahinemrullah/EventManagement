using EventManagement.Domain.Collections;
using EventManagement.Persistence;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using EventManagement.Domain.Common;
using EventManagement.Application.Features.Common.Extensions;
using EventManagement.Application.Features.Common.Exceptions;
using EventManagement.Domain.Validations.Interfaces;

namespace EventManagement.Application.Features.DefinitionEntities
{
    public sealed class DefinitionEntityService<TDefinitionEntity, TCreator> : IDefinitionEntityService<TDefinitionEntity>
        where TDefinitionEntity : class, IDefinitionEntity
        where TCreator : IDefinitionEntityCreator<TDefinitionEntity>
    {
        private readonly DbSet<TDefinitionEntity> _dbSet;
        private readonly IResultFactory _resultFactory;
        private readonly IDefinitionEntityCreator<TDefinitionEntity> _definitionEntityCreator;
        public DefinitionEntityService(EventManagementDbContext dbContext, IDefinitionEntityCreator<TDefinitionEntity> definitionEntityCreator, IResultFactory resultFactory)
        {
            _dbSet = dbContext.Set<TDefinitionEntity>();
            _resultFactory = resultFactory;
            _definitionEntityCreator = definitionEntityCreator;
        }

        public IResult<TDefinitionEntity> Create(string name)
        {
            TDefinitionEntity? existingdefinitionEntity = _dbSet.SingleOrDefault(c => c.Name == name);

            if (existingdefinitionEntity is not null)
                return _resultFactory.Failure<TDefinitionEntity>(new EntityExistsException(typeof(TDefinitionEntity).Name));

            IResult<TDefinitionEntity> result = _definitionEntityCreator.FromName(name);

            if (!result.IsSuccess)
                return result;

            EntityEntry<TDefinitionEntity> entry = _dbSet.Add(result.Value!);

            TDefinitionEntity entity = entry.Entity;

            return _resultFactory.Success(entity);
        }

        public IResult Delete(int id)
        {
            TDefinitionEntity? definitionEntity = _dbSet.Find(id);

            if (definitionEntity is null)
                return _resultFactory.Failure(new NotFoundException(id.ToString(), typeof(TDefinitionEntity).Name));

            _dbSet.Remove(definitionEntity);

            return _resultFactory.Success();
        }

        public IEnumerable<TDefinitionEntity> GetAll()
        {
            return _dbSet.ToHashSet();
        }

        public IResult<TDefinitionEntity> GetById(int id)
        {
            TDefinitionEntity? definitionEntity = _dbSet.Find(id);

            if (definitionEntity is null)
                return _resultFactory.Failure<TDefinitionEntity>(new NotFoundException(id.ToString(), typeof(TDefinitionEntity).Name));

            return _resultFactory.Success(definitionEntity);
        }

        public IPaginatedList<TDefinitionEntity> GetPaginated(int pageNumber, int pageSize)
        {
            return _dbSet
                    .OrderBy(c => c.Id)
                    .ToPaginatedList(pageSize, pageNumber);
        }

        public IResult Update(int id, string name)
        {
            TDefinitionEntity? definitionEntity = _dbSet.Find(id);

            if (definitionEntity is null)
                return _resultFactory.Failure(new NotFoundException(id.ToString(), typeof(TDefinitionEntity).Name));

            TDefinitionEntity? existingEntity = _dbSet.Where(d => d.Name == name).FirstOrDefault();

            if(existingEntity is not null)
                return _resultFactory.Failure(new EntityExistsException(typeof(TDefinitionEntity).Name));

            IResult result = definitionEntity.SetName(name);

            if (!result.IsSuccess)
                return result;

            _dbSet.Update(definitionEntity);

            return result;
        }
    }
}
