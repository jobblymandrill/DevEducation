using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IOrderStorage
    {
        ValueTask<Order> AddOrder(Order dataModel);
        ValueTask FillProducts(Order dataModel);
        ValueTask<Order> GetOrderById(long id);
        void TransactionStart();
        void TransactionCommit();
        void TransactioRollBack();
    }
}