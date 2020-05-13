using ElecronicsStore.DB.Models;
using ElecronicsStore.DB.Storages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public class ReportRepository : IReportRepository
    {
        private IReportStorage _reportStorage;

        public ReportRepository(IReportStorage reprotStorage)
        {
            _reportStorage = reprotStorage;
        }

        public async ValueTask<RequestResult<List<Product>>> GetNeverOrderedProducts()
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetNeverOrderedProducts();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<CategoryWithNumber>>> GetCategoriesWithACertainProductNumber(int number)
        {
            var result = new RequestResult<List<CategoryWithNumber>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetCategoriesWithACertainProductNumber(number);
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<Product>>> GetProductsFromWareHouseNotPresentInMscAndSpb()
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetProductsFromWareHouseNotPresentInMscAndSpb();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<Product>>> GetRanOutProducts()
        {
            var result = new RequestResult<List<Product>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetRanOutProducts();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<ProductWithCity>>> GetMostPopularProductInEachCity()
        {
            var result = new RequestResult<List<ProductWithCity>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetMostPopularProductInEachCity();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<FilialWithIncome>>> GetTotalFilialSumPerPeriod(Period dataModel)
        {
            var result = new RequestResult<List<FilialWithIncome>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetTotalFilialSumPerPeriod(dataModel);
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<IncomeByIsForeignCriteria>> GetIncomeFromRussiaAndFromForeignCountries()
        {
            var result = new RequestResult<IncomeByIsForeignCriteria>();                                                                                            
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetIncomeFromRussiaAndFromForeignCountries();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<FilialWithIncome>>> GetIncomeFromEachFilial()
        {
            var result = new RequestResult<List<FilialWithIncome>>();
            try
            {
                _reportStorage.TransactionStart();
                result.RequestData = await _reportStorage.GetIncomeFromEachFilial();
                _reportStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _reportStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
