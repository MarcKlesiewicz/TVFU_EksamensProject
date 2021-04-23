using System;
using System.Collections.Generic;

namespace Persistence.Repositories.Interfaces
{
    //En abstrakt repository interface som indeholder de nødvendige CRUD metoder
    public interface IRepo<TEntity> where TEntity : class
    {
        void Add(EventArgs args);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetBy(EventArgs args);

        TEntity Get(string guid);

        void Remove(string guid);

        void Update(EventArgs args);
    }
}
