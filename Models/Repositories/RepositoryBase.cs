using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventFinder.Models.EntitiesAbstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventFinder.Models.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : EntityBase
    {
        public RepositoryBase(DbContext context, ILogger<RepositoryBase<TEntity>> logger)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
            this.Logger = logger;
        }
        protected DbContext Context {get;set;}
        protected DbSet<TEntity> DbSet{get;set;}
        protected ILogger<RepositoryBase<TEntity>> Logger{get;set;}

        public void Delete(int id)
        {
            Delete(Find(id));
            Context.SaveChanges();
        }

        public void Delete(TEntity entityToDelete)
        {
            if(Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public bool Exist(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Any(filter);
        }

        public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.AnyAsync(filter);
        }

        public TEntity Find(int id)
        {
            return DbSet.Find(id);
        }

        public Task<TEntity> FindAsync(int id)
        {
            return DbSet.FindAsync(id).AsTask();
        }

        public void Flush()
        {
            Context.SaveChanges();
        }

        public Task FlushAsync()
        {
            return Context.SaveChangesAsync();
        }

        public int Insert(TEntity entity)
        {
            try{
                var result = DbSet.Add(entity).Entity;
                Context.SaveChanges();
                return result.Id;
            }catch(Exception){
                throw;
            }
        }
        
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> Query = DbSet;
            return DbSet.Where(filter);
        }

        public Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(()=>{return Query(filter);});
        }

        public void Update(int id, Action<TEntity> updateAction)
        {
            var entity = DbSet.Find(id) ?? throw new Exception("Entity not found");

            updateAction(entity);

            Context.Entry(entity).State = EntityState.Modified;

            Context.SaveChanges();
        }

        public void UpdateEntity(TEntity entityToUpdate)
        {
            if(Context.Entry(entityToUpdate).State == EntityState.Detached){
                DbSet.Attach(entityToUpdate);
            }
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}