using ElecronicsStore.DB.Models;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public interface IUserRepository
    {
        ValueTask<RequestResult<string>> AddUser(User dataModel);
        ValueTask<User> GetUserByEmailAndPassword(User dataModel);
    }
}