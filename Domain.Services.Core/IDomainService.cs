using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Domain.Services.Core {
    public interface IDomainService<TEntity, Key>
        where TEntity : class {
        IList<TEntity> GetAll();
        TEntity GetOne(Key id);
        IList<TResult> Get<TResult>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TResult>> selector = null);
        void Add(TEntity item);
        void Modify(TEntity item);
        void Remove(TEntity item);
        IEnumerable<ValidationResult> GetValidationErrors(TEntity item);
        bool IsValid(TEntity item);
    }
}
