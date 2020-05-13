using Dapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.Core.ConfigurationOptions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElecronicsStore.DB.Storages
{
    public class ReportStorage : IReportStorage
    {
        private IDbConnection _connection;

        private IDbTransaction _transaction;

        public ReportStorage(IOptions<StorageOptions> storageOptions)
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
            public const string GetNeverOrderedProducts = "GetNeverOrderedProducts";
            public const string ShowCategoriesWhereProductNumberIsMoreThanSomeNumber = "ShowCategoriesWhereProductNumberIsMoreThanSomeNumber";
            public const string GetProductsFromWareHouseNotPresentInMscAndSpb = "GetProductsFromWareHouseNotPresentInMscAndSpb";
            public const string GetRanOutProducts = "GetRanOutProducts";
            public const string GetMostPopularProductInEachCity = "GetMostPopularProductInEachCity";
            public const string GetTotalFilialSumPerPeriod = "GetTotalFilialSumPerPeriod";
            public const string GetIncomeFromRussiaAndFromForeignCountries = "GetIncomeFromRussiaAndFromForeignCountries";
            public const string GetIncomeFromEachFilial = "GetIncomeFromEachFilial";
        }

        public async ValueTask<List<Product>> GetNeverOrderedProducts()
        {
            try
            {
                var result = await _connection.QueryAsync<Product, Category, Category, Product>(
                            SpName.GetNeverOrderedProducts,
                            (product, category, parentCategory) =>
                            {
                                Product newProduct = product;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newProduct.Category = newCategory;
                                return newProduct;
                            },
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id, Id");
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<CategoryWithNumber>> GetCategoriesWithACertainProductNumber(int number)
        {
            try
            {
                var result = await _connection.QueryAsync<CategoryWithNumber, Category, Category, CategoryWithNumber>(
                            SpName.ShowCategoriesWhereProductNumberIsMoreThanSomeNumber,
                            (categoryWithNumber, category, parentCategory) =>
                            {
                                CategoryWithNumber newCategoryWithNumber = categoryWithNumber;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newCategoryWithNumber.Category = newCategory;
                                return newCategoryWithNumber;
                            },
                            param: new { number },
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id");
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<Product>> GetProductsFromWareHouseNotPresentInMscAndSpb()
        {
            try
            {
                var result = await _connection.QueryAsync<Product, Category, Category, Product>(
                            SpName.GetProductsFromWareHouseNotPresentInMscAndSpb,
                            (product, category, parentCategory) =>
                            {
                                Product newProduct = product;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newProduct.Category = newCategory;
                                return newProduct;
                            },
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id, Id");
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<Product>> GetRanOutProducts()
        {
            try
            {
                var result = await _connection.QueryAsync<Product, Category, Category, Product>(
                            SpName.GetRanOutProducts,
                            (product, category, parentCategory) =>
                            {
                                Product newProduct = product;
                                Category newCategory = category;
                                Category newParentCategory = parentCategory;
                                newCategory.ParentCategory = newParentCategory;
                                newProduct.Category = newCategory;
                                return newProduct;
                            },
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure,
                            splitOn: "Id, Id, Id");
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<ProductWithCity>> GetMostPopularProductInEachCity()
        {
            try
            {
                var result = await _connection.QueryAsync<ProductWithCity>(
                            SpName.GetMostPopularProductInEachCity,
                            null,
                            transaction: _transaction,
                            commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<FilialWithIncome>> GetTotalFilialSumPerPeriod(Period dataModel)
        {
            try
            {
                DynamicParameters periodModelParams = new DynamicParameters(new
                {
                    dataModel.Start,
                    dataModel.End,
                });
                var result = await _connection.QueryAsync<FilialWithIncome>(
                                SpName.GetTotalFilialSumPerPeriod,
                                periodModelParams,
                                transaction: _transaction,
                                commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IncomeByIsForeignCriteria> GetIncomeFromRussiaAndFromForeignCountries()
        {
            try
            {
                var result = await _connection.QueryAsync<IncomeByIsForeignCriteria>(
                           SpName.GetIncomeFromRussiaAndFromForeignCountries,
                           null,
                           transaction: _transaction,
                           commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async ValueTask<List<FilialWithIncome>> GetIncomeFromEachFilial()
        {
            try
            {
                var result = await _connection.QueryAsync<FilialWithIncome>(
                           SpName.GetIncomeFromEachFilial,
                           null,
                           transaction: _transaction,
                           commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
