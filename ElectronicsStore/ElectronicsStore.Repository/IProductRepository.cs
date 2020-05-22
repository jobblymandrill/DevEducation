using ElecronicsStore.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public interface IProductRepository
    {
        ValueTask<RequestResult<List<Product>>> ProductSearch(ProductSearch dataModel);
    }
}