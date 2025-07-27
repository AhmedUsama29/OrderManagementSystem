using Domain.Contracts;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UnitOfWork(OrderManagementDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {

            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
                return (GenericRepository<TEntity>)_repositories[typeName];


            var repo = new GenericRepository<TEntity>(_dbContext);

            _repositories[typeName] = repo;

            return repo;
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
