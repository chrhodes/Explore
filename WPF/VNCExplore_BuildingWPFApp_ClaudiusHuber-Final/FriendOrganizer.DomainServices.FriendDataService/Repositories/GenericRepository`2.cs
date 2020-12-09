using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VNC;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.DomainServices.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext Context;

        // Can only be used as a base class - protected

        protected GenericRepository(TContext context)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Context = context;

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        public void Add(TEntity model)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Context.Set<TEntity>().Add(model);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }

        // Can be overridden in derived classes - virtual
        public virtual async Task<TEntity> FindByIdAsync(int id)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);

            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);

            return await Context.Set<TEntity>().ToListAsync();
        }

        public bool HasChanges()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
            var result = Context.ChangeTracker.HasChanges();
            return result;
        }

        public void Remove(TEntity model)
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);

            Context.Set<TEntity>().Remove(model);
        }

        public async Task UpdateAsync()
        {
            Int64 startTicks = Log.Trace(String.Format("Enter"), Common.LOG_APPNAME);

            await Context.SaveChangesAsync();

            Log.Trace(String.Format("Exit"), Common.LOG_APPNAME, startTicks);
        }
    }
}
