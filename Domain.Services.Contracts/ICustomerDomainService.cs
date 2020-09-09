using Domain.Entities;
using Domain.Services.Core;
using System;
using System.Collections.Generic;

namespace Domain.Services.Contracts {
    public interface ICustomerDomainService : IDomainService<Customer, int> {
        IList<Customer> GetPage(int numPage = 0, int pageSize = 20);
    }
}
