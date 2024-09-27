using Dnc.Services.Employee.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Services.Employee.Repositories.Abstract
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public Task<int> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id, byte[] timeStamp)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void ExecuteParameterizedQuery(string sql, object[] sqlParametersObjects)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ExecuteSqlStringAsync(string sql)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindIgnoreQueryFiltersAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllIgnoreQueryFiltersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsNoTrackingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
