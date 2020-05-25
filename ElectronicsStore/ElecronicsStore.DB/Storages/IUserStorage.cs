using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public interface IUserStorage
    {
        ValueTask AddUser(User dataModel);
        ValueTask<User> GetUserByEmailAndPassword(User dataModel);
        void TransactionCommit();
        void TransactionStart();
        void TransactioRollBack();
    }
}