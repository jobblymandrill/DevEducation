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
    public class OrderStorage : IOrderStorage
    {
        private IDbConnection _connection;

        private IDbTransaction _transaction;

        private IProductStorage _productStorage;

        public OrderStorage(IOptions<StorageOptions> storageOptions, IProductStorage productStorage)
        {
            _connection = new SqlConnection(storageOptions.Value.DBConnectionString);
            _productStorage = productStorage;
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
            public const string OrderAdd = "Order_Add";
            public const string GetOrderById = "Order_GetById";
            public const string GetProductById = "Product_GetById";
            public const string OrderProductAmountAdd = "Order_Product_Amount_Add";
            public const string BatchInsert = "Order_Product_Amount_BatchInsert";
            public const string BatchInsert1 = "Order_BatchInsert";
        }

        public async ValueTask<Order> AddOrder(Order dataModel)
        {
            try
            {
                DynamicParameters dataModelParams = new DynamicParameters(new
                {
                    dataModel.FilialId,
                    dataModel.FilialCity
                });
                var result = await _connection.QueryAsync<long>(
                       SpName.OrderAdd,
                       dataModelParams,
                       transaction: _transaction,
                       commandType: CommandType.StoredProcedure);
                dataModel.Id = result.FirstOrDefault();
                dataModel.DateTime = GetOrderById((long)dataModel.Id).Result.DateTime;
                await FillProducts(dataModel);
                return dataModel;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask FillProducts(Order dataModel)
        {
            foreach (var item in dataModel.Products)
            {
                long productId = item.Product.Id;
                item.OrderId = dataModel.Id;
                try
                {
                    DynamicParameters itemParams = new DynamicParameters(new
                    {
                        productId,
                        item.OrderId,
                        item.Amount
                    });
                    var result = await _connection.QueryAsync<int>(
                        SpName.OrderProductAmountAdd,
                        itemParams,
                        transaction: _transaction,
                        commandType: CommandType.StoredProcedure);
                    item.Id = result.FirstOrDefault();
                    item.Product.Name = _productStorage.GetProductById(item.Product.Id).Result.Name;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public async ValueTask<Order> GetOrderById(long id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters(new
                {
                    id
                });
                var result = await _connection.QueryAsync<Order>(
                        SpName.GetOrderById,
                        param,
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
