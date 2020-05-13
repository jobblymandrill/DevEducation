using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using ElectronicsStore.Core;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase, IReportController
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        [HttpGet("category/with-more-than-{number}-products")]
        public async ValueTask<ActionResult<List<CategoryWithNumberOutputModel>>> GetCategoriesWithACertainProductNumber(int number)
        {
            if (number < 1) { return BadRequest("Incorrect number"); }
            var result = await _reportRepository.GetCategoriesWithACertainProductNumber(number);
            if (result.IsOkay)
            {
                if (result.RequestData == null)
                {
                    return Problem($"There is no categories with this number of products.", statusCode: 520);
                }
                return Ok(_mapper.Map<List<CategoryWithNumberOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("product/from-warehouse-not-present-in-msc-and-spb")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsFromWareHouseNotPresentInMscAndSpb()
        {
            var result = await _reportRepository.GetProductsFromWareHouseNotPresentInMscAndSpb();
            if (result.IsOkay)
            {
                if (result.RequestData == null)
                {
                    return Problem($"There is no products from warehouse not present in Msc and Spb.", statusCode: 520);
                }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("product/finished")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetRanOutProducts()
        {
            var result = await _reportRepository.GetRanOutProducts();
            if (result.IsOkay)
            {
                if (result.RequestData == null)
                {
                    return Problem($"There is no ran out products.", statusCode: 520);
                }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("product/most-popular-in-each-city")]
        public async ValueTask<ActionResult<List<ProductWithCityOutputModel>>> GetMostPopularProductInEachCity()
        {
            var result = await _reportRepository.GetMostPopularProductInEachCity();
            if (result.IsOkay)
            {
                if (result.RequestData == null)
                {
                    return Problem($"Sorry, there is no information about most popular products.", statusCode: 520);
                }
                return Ok(_mapper.Map<List<ProductWithCityOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("product/never-ordered")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetNeverOrderedProducts()
        {
            var result = await _reportRepository.GetNeverOrderedProducts();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return Problem($"List of never ordered products is empty.", statusCode: 520); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("income/from-russia-and-foreign-countries")]
        public async ValueTask<ActionResult<IncomeByIsForeignCriteriaOutputModel>> GetIncomeFromRussiaAndFromForeignCountries()
        {
            var result = await _reportRepository.GetIncomeFromRussiaAndFromForeignCountries();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return Problem($"Sorry, there is no information about income from Russia and from other countries.", statusCode: 520); }
                return Ok(_mapper.Map<IncomeByIsForeignCriteriaOutputModel>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("income/from-each-filial")]
        public async ValueTask<ActionResult<List<FilialWithIncomeOutputModel>>> GetIncomeFromEachFilial()
        {
            var result = await _reportRepository.GetIncomeFromEachFilial();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return Problem($"Sorry, there is no information about income from each filial.", statusCode: 520); }
                return Ok(_mapper.Map<List<FilialWithIncomeOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("income/of-filials-during-period")]
        public async ValueTask<ActionResult<List<FilialWithIncomeOutputModel>>> GetTotalFilialSumPerPeriod(PeriodInputModel inputModel)
        {
            if (!DateValidation.IsValidDate(inputModel.StartDate)) { return BadRequest("StartDate should be in dd.MM.yyyy format."); }
            if (!DateValidation.IsValidDate(inputModel.EndDate)) { return BadRequest("EndDate should be in dd.mm.yyyy format."); }
            var result = await _reportRepository.GetTotalFilialSumPerPeriod(_mapper.Map<Period>(inputModel));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return Problem($"Sorry, there is no information about total filial sum per period.", statusCode: 520); }
                return Ok(_mapper.Map<List<FilialWithIncomeOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}
