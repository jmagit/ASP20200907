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
    public class CustomerRepository : ICustomerRepository {
        private readonly TiendaDbContext context;

        public CustomerRepository(TiendaDbContext context) {
            this.context = context;
        }

        public IList<Customer> GetAll() {
            return context.Customer.ToList();
        }

        public Customer GetOne(int id) {
            return context.Customer.Find(id);
        }

        public IList<TResult> Get<TResult>(Expression<Func<Customer, bool>> predicate = null, Expression<Func<Customer, TResult>> selector = null) {
            var query = context.Set<Customer>().AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (selector != null)
                return query.Select(selector).ToList();
            return query.ToList() as IList<TResult>;
        }
        public IList<Customer> GetPage(int numPage = 0, int pageSize = 20) {
            return context.Customer
                .OrderBy(o => o.FirstName).OrderBy(o => o.LastName)
                .Skip(numPage * pageSize).Take(pageSize)
                .ToList();
        }

        public void Add(Customer item) {
            context.Add(item);
            context.SaveChanges();
        }

        public void Modify(Customer item) {
            context.Update(item);
            context.SaveChanges();
        }

        public void Remove(Customer item) {
            context.Remove(item);
            context.SaveChanges();
        }
    }
}
