using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    public interface IReportController
    {
        ValueTask<ActionResult<List<ProductOutputModel>>> GetNeverOrderedProducts();
        ValueTask<ActionResult<List<CategoryWithNumberOutputModel>>> GetCategoriesWithACertainProductNumber(int number);
        ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsFromWareHouseNotPresentInMscAndSpb();
        ValueTask<ActionResult<List<ProductOutputModel>>> GetRanOutProducts();
        ValueTask<ActionResult<List<FilialWithIncomeOutputModel>>> GetTotalFilialSumPerPeriod(PeriodInputModel inputModel);
        ValueTask<ActionResult<IncomeByIsForeignCriteriaOutputModel>> GetIncomeFromRussiaAndFromForeignCountries();
        ValueTask<ActionResult<List<FilialWithIncomeOutputModel>>> GetIncomeFromEachFilial();
    }
}