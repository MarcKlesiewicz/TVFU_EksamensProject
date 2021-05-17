using System.Collections.Generic;

namespace Persistence.Repositories.Interfaces
{
    public interface IGetAll<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}
