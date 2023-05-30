using Complaint_API.Contexts;
using Complaint_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Complaint_API.Repository
{
    public class GeneralRepository<TEntity, TKey, TContext> : IGeneralRepository<TEntity, TKey>
        where TEntity : class
        where TContext : MyContext
    {
        protected TContext _context;
        public GeneralRepository(TContext context)
        {
            _context = context;
        }

        // GET ALL
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }


        // GET BY ID
        public virtual async Task<TEntity?> GetByIdAsync(TKey key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }

        // INSERT
        public virtual async Task<TEntity?> InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // UPDATE
        public async Task<int> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task<int> DeleteAsync(TKey key)
        {
            var entity = await GetByIdAsync(key);
            if (entity == null)
            {
                return 0;
            }
            _context.Set<TEntity>().Remove(entity!);
            return await _context.SaveChangesAsync();
        }

        // IS EXIST
        public virtual async Task<bool> IsExist(TKey key)
        {
            var entity = await GetByIdAsync(key);
            _context.ChangeTracker.Clear();
            return entity != null;
        }
    }
}
