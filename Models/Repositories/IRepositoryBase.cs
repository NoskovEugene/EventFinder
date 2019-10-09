using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventFinder.Models.EntitiesAbstraction;

namespace EventFinder.Models.Repositories
{
    public interface IRepositoryBase<TEntity>
    where TEntity : EntityBase
    {
        bool Exist(Expression<Func<TEntity,bool>> filter);

        Task<bool> ExistAsync(Expression<Func<TEntity,bool>> filter);

        IQueryable<TEntity> Query(Expression<Func<TEntity,bool>> filter);

        Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity,bool>> filter);

        TEntity Find(int id);

        Task<TEntity> FindAsync(int id);

        int Insert(TEntity entity);

        void Update(int id, Action<TEntity> updateAction);

        void UpdateEntity(TEntity entityToUpdate);

        void Delete(int id);

        void Delete(TEntity entityToDelete);

        void Flush();

        Task FlushAsync();
    }
}