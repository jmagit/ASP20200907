using Domain.Entities;
using Infrastructure.Data.Core;
using Infrastructure.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Contracts.Repositories;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories {
    public class ProductRepository : IProductRepository {
        private readonly TiendaDbContext context;

        public ProductRepository(TiendaDbContext context) {
            this.context = context;
        }

        public IList<Product> GetAll() {
            return context.Product.ToList();
        }

        public Product GetOne(int id) {
            return context.Product.Find(id);
        }

        public IList<TResult> Get<TResult>(Expression<Func<Product, bool>> predicate = null, Expression<Func<Product, TResult>> selector = null) {
            var query = context.Set<Product>().AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (selector != null)
                return query.Select(selector).ToList();
            return query.ToList() as IList<TResult>;
        }

        public void Add(Product item) {
            context.Add(item);
            context.SaveChanges();
        }

        public void Modify(Product item) {
            context.Update(item);
            context.SaveChanges();
        }

        public void Remove(Product item) {
            context.Remove(item);
            context.SaveChanges();
        }
    }
}
