using ElecronicsStore.DB.Models;
using ElecronicsStore.DB.Storages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IProductStorage _productStorage;

        public ProductRepository(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }

        public async ValueTask<RequestResult<List<Product>>> ProductSearch(ProductSearch dataModel)
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                _productStorage.TransactionStart();
                result.RequestData = await _productStorage.ProductSearch(dataModel);
                _productStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _productStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
    
