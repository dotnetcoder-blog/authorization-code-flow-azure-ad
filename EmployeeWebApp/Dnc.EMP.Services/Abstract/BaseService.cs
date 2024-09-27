using Dnc.EMP.DAL;
using Dnc.EMP.DAL.Models;
using Dnc.EMP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.EMP.Services.Abstract
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        public AppDbContext Context { get; }
        public DbSet<T> Table { get; }

        protected BaseService(AppDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Table.AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIgnoreQueryFiltersAsync()
        {
            return await Table.AsQueryable().IgnoreQueryFilters().ToListAsync();
        }

        public async Task<IEnumerable<T>> ExecuteSqlStringAsync(string sql)
        {
            return await Table.FromSqlRaw(sql).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> GetAsNoTrackingAsync(int id)
        {
            return await Table.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> FindIgnoreQueryFiltersAsync(int id)
        {
            return await Table.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task ExecuteParameterizedQuery(string sql, object[] sqlParametersObjects)
        {
            await Context.Database.ExecuteSqlRawAsync(sql, sqlParametersObjects);
        }

        public async Task<int> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            Table.Update(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            Table.UpdateRange(entities);
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            Table.Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
            return await SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
