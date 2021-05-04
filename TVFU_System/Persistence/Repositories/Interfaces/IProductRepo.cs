using System.Collections;

namespace Persistence.Repositories.Interfaces
{
    public interface IProductRepo : IRepo<DomainLayer.Models.Product>
    {
        IEnumerable GetByProductCategories();

        IEnumerable GetByProductNumber(int min, int max);

        IEnumerable SearchProductList(string searchCategory, string searchWord);

        IEnumerable SortAfter(string sortCategory, string order);
    }
}
