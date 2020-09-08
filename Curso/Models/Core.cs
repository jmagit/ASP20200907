using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Domain.Entities.Core {
    public abstract class Entity {

    }
}
namespace Domain.Services.Core {
    public interface IDomainService<TEntity, Key>
        where TEntity : class {
        List<TEntity> GetAll();
        TEntity GetOne(Key id);
        void Add(TEntity item);
        void Modify(TEntity item);
        void Remove(TEntity item);
    }
}
namespace Domain.Core {
    public interface IRepository<TEntity, Key>
        where TEntity : class {
        List<TEntity> GetAll();
        TEntity GetOne(Key id);
        void Add(TEntity item);
        void Modify(TEntity item);
        void Remove(TEntity item);
    }
}
namespace Infrastructure.Data.Core.Repositories {
}
