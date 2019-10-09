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
            return DbSet.FindAsync(id);
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public Task FlushAsync()
        {
            throw new NotImplementedException();
        }

        public int Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Action<TEntity> updateAction)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}