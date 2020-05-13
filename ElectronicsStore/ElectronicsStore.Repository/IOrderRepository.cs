using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public interface IOrderRepository
    {
        ValueTask<RequestResult<Order>> AddOrder(Order dataModel);
    }
}