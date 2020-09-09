using Domain.Entities;
using Domain.Services.Core;
using System;

namespace Domain.Services.Contracts {
    public interface IProductDomainService: IDomainService<Product, int> {
    }
}
