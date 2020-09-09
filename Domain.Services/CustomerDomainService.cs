using Domain.Entities;
using Domain.Services.Contracts;
using Domain.Services.Core;
using Infrastructure.Data.Contracts.Repositories;
using Infrastructure.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services {
    public class CustomerDomainService: DomainServiceBase<Customer, int>, ICustomerDomainService {
        public CustomerDomainService(ICustomerRepository repository): base(repository) {

        }

        public IList<Customer> GetPage(int numPage = 0, int pageSize = 20) {
            return (repository as ICustomerRepository).GetPage(numPage, pageSize);
        }

        public void CambiaContraseña(Customer item, string nueva) {
            item.CambiaContraseña(nueva);
            repository.Modify(item);
        }

    }
}
