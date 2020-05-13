using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IProductStorage
    {
        ValueTask<Product> GetProductById(long id);
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}