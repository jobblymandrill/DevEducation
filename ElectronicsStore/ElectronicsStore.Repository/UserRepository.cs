using ElecronicsStore.DB.Models;
using ElecronicsStore.DB.Storages;
using System;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private IUserStorage _userStorage;

        public UserRepository(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public async ValueTask<RequestResult<string>> AddUser(User dataModel)
        {
            var result = new RequestResult<string>();
            try
            {
                _userStorage.TransactionStart();
                await _userStorage.AddUser(dataModel);
                _userStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _userStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<User> GetUserByEmailAndPassword(User dataModel)
        {
            var result = new User();
            try
            {
                _userStorage.TransactionStart();
                result = await _userStorage.GetUserByEmailAndPassword(dataModel);
                _userStorage.TransactionCommit();
            }
            catch (Exception ex)
            {
                _userStorage.TransactioRollBack();
            }
            if(result.Password == dataModel.Password)
            {
                return result;
            }
            return null;//здесь надо сделать, чтобы нормально возвращало сообщение, что пользователь не найден
        }
    }
}
