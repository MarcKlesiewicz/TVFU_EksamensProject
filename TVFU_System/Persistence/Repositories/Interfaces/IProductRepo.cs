using System.Collections;

namespace Persistence.Repositories.Interfaces
{
    public interface IProductRepo : IRepo<DomainLayer.Models.Product>
    {
        IEnumerable GetByProductCategories();
    }
}
