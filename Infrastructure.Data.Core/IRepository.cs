using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Data.Core {
    public interface IRepository<TEntity, Key>
        where TEntity : class {
        IList<TEntity> GetAll();
        TEntity GetOne(Key id);
        IList<TResult> Get<TResult>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TResult>> selector = null);
        void Add(TEntity item);
        void Modify(TEntity item);
        void Remove(TEntity item);
    }
}
