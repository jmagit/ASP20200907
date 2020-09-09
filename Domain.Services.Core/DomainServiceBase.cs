using Infrastructure.Data.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Services.Core {
    public abstract class DomainServiceBase<TEntity, Key> : IDomainService<TEntity, Key> where TEntity : class {
        protected readonly IRepository<TEntity, Key> repository;

        public DomainServiceBase(IRepository<TEntity, Key> repository) {
            this.repository = repository;
        }

        public virtual IList<TEntity> GetAll() {
            return repository.GetAll();
        }

        public virtual TEntity GetOne(Key id) {
            return repository.GetOne(id);
        }

        public IList<TResult> Get<TResult>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TResult>> selector = null) {
            return repository.Get<TResult>(predicate, selector);
        }

        public virtual void Add(TEntity item) {
            if (IsValid(item)) {
                repository.Add(item);
            } else {
                throw new ArgumentException();
            }
        }

        public virtual void Modify(TEntity item) {
            if (IsValid(item)) {
                repository.Modify(item);
            } else {
                throw new ArgumentException();
            }
        }

        public virtual void Remove(TEntity item) {
            if (IsValid(item)) {
                repository.Remove(item);
            } else {
                throw new ArgumentException();
            }
        }

        public virtual IEnumerable<ValidationResult> GetValidationErrors(TEntity item) {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(item, null, null);
            Validator.TryValidateObject(item,
                      context,
                      validationResults,
                      true);
            return validationResults;
        }
        public virtual bool IsValid(TEntity item) {
            return item != null && GetValidationErrors(item).Count() == 0;
        }
    }
}
