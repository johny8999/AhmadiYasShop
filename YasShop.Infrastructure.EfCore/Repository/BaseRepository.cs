using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasShop.Infrastructure.EfCore.Context;

namespace YasShop.Infrastructure.EfCore.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly MainContext _context;
        public BaseRepository(MainContext context)
        {
            _context = context;
            DbEntities = _context.Set<TEntity>();
        }

        public DbSet<TEntity> DbEntities { get; }

        public IQueryable<TEntity> Get => DbEntities;

        public IQueryable<TEntity> GetNoTraking => DbEntities.AsNoTracking();

        public virtual async Task AddAsync(TEntity entity, bool AutoSave = true)
        {
            await DbEntities.AddAsync(entity);
            if (AutoSave)
            {
                await _context.SaveChangesAsync();
            }

        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, bool AutoSave = true)
        {
            await DbEntities.AddRangeAsync(entities);
            if (AutoSave)
            {
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteAsync(TEntity entity, bool AutoSave = true)
        {
            DbEntities.Remove(entity);
            if (AutoSave)
            {
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool AutoSave = true)
        {
            DbEntities.RemoveRange(entities);
            if (AutoSave)
            {
                await _context.SaveChangesAsync();
            }

        }

        public async Task<TEntity> GetById(params object[] Id)
        {
            return await DbEntities.FindAsync(Id);
        }

        public async Task UpdateAsync(TEntity entity, bool AutoSave = true)
        {
            DbEntities.Update(entity);
            if (AutoSave)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool AutoSave = true)
        {
            DbEntities.UpdateRange(entities);
            if (AutoSave)
            {
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
