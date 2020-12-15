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
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            Context = context;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        public void Add(TEntity model)
        {
            Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

            Context.Set<TEntity>().Add(model);

            Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        }

        // Can be overridden in derived classes - virtual
        public virtual async Task<TEntity> FindByIdAsync(int id)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(GenericRepository<T>) Enter", Common.LOG_APPNAME);

            var result = await Context.Set<TEntity>().FindAsync(id);

            Log.DOMAINSERVICES("(GenericRepository<T>) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            Int64 startTicks = Log.DOMAINSERVICES("(GenericRepository<T>) Enter", Common.LOG_APPNAME);

            var result =  await Context.Set<TEntity>().ToListAsync();

            Log.DOMAINSERVICES("(GenericRepository<T>) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public bool HasChanges()
        {
            Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

            var result = Context.ChangeTracker.HasChanges();

            Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        public void Remove(TEntity model)
        {
            Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

            Context.Set<TEntity>().Remove(model);

            Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        }

        public async Task UpdateAsync()
        {
            Int64 startTicks = Log.DOMAINSERVICES("(GenericRepository<T>) Enter", Common.LOG_APPNAME);

            await Context.SaveChangesAsync();

            Log.DOMAINSERVICES(String.Format("(GenericRepository<T>) Exit"), Common.LOG_APPNAME, startTicks);
        }
    }
}
