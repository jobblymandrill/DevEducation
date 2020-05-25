using Dapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.Core.ConfigurationOptions;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public class UserStorage : IUserStorage
    {
        private IDbConnection _connection;

        private IDbTransaction _transaction;

        public UserStorage(IOptions<StorageOptions> storageOptions)
        {
            _connection = new SqlConnection(storageOptions.Value.DBConnectionString);
        }

        public void TransactionStart()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void TransactionCommit()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void TransactioRollBack()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        internal static class SpName
        {
            public const string UserAdd = "User_Add";
            public const string UserGetByEmailAndPassword = "User_Get_By_Email_And_Password";
        }

        public async ValueTask AddUser(User dataModel)
        {
            try
            {
                DynamicParameters dataModelParams = new DynamicParameters(new
                {
                    dataModel.FirstName,
                    dataModel.LastName,
                    dataModel.Email,
                    dataModel.Phone,
                    dataModel.Password
                });

                var result = await _connection.QueryAsync<long>(
                    SpName.UserAdd,
                    dataModelParams,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
                dataModel.Id = result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<User> GetUserByEmailAndPassword(User dataModel)
        {
            try
            {
                DynamicParameters dataModelParams = new DynamicParameters(new
                {
                    dataModel.Email,
                    dataModel.Password
                });
                var result = await _connection.QueryAsync<User>(
                    SpName.UserGetByEmailAndPassword,
                    dataModelParams,
                    transaction: _transaction,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
