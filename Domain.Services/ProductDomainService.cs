using Domain.Entities;
using Domain.Services.Contracts;
using Domain.Services.Core;
using Infrastructure.Data.Contracts.Repositories;
using Infrastructure.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services {
    public class ProductDomainService : DomainServiceBase<Product, int>, IProductDomainService {
        public ProductDomainService(IProductRepository repository): base(repository) {

        }
    }
}
