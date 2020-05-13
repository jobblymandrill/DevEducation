using ElecronicsStore.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public interface IReportRepository
    {
        ValueTask<RequestResult<List<Product>>> GetNeverOrderedProducts();
        ValueTask<RequestResult<List<CategoryWithNumber>>> GetCategoriesWithACertainProductNumber(int number);
        ValueTask<RequestResult<List<Product>>> GetProductsFromWareHouseNotPresentInMscAndSpb();
        ValueTask<RequestResult<List<Product>>> GetRanOutProducts();
        ValueTask<RequestResult<List<ProductWithCity>>> GetMostPopularProductInEachCity();
        ValueTask<RequestResult<List<FilialWithIncome>>> GetTotalFilialSumPerPeriod(Period filialNameWithPeriodDataModel);
        ValueTask<RequestResult<IncomeByIsForeignCriteria>> GetIncomeFromRussiaAndFromForeignCountries();
        ValueTask<RequestResult<List<FilialWithIncome>>> GetIncomeFromEachFilial();
    }
}