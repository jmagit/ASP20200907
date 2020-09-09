using Domain.Entities;
using Infrastructure.Data.Core;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Contracts.Repositories {
    public interface ICustomerRepository : IRepository<Customer, int> {
        IList<Customer> GetPage(int numPage = 0, int pageSize = 20);
    }
    public interface IProductRepository : IRepository<Product, int> {
    }
}
