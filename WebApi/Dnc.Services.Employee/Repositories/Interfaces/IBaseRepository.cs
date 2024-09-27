using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Services.Employee.Repositories.Interfaces
{
    internal interface IBaseRepository<T> where T : class, new()
    {
        //ApplicationDbContext Context { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllIgnoreQueryFiltersAsync();
        Task<IEnumerable<T>> ExecuteSqlStringAsync(string sql);
        Task<T> GetByIdAsync(int id);
        Task<T> GetAsNoTrackingAsync(int id);
        Task<T> FindIgnoreQueryFiltersAsync(int id);
        void ExecuteParameterizedQuery(string sql, object[] sqlParametersObjects);
        Task<int> AddAsync(T entity);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities);
        Task<int> DeleteAsync(int id, byte[] timeStamp);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteRangeAsync(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
    }
}
