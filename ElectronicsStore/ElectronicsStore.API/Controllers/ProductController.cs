using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase, IProductController
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> ProductSearch(ProductSearchInputModel inputModel)
        {
            if (inputModel == null) { return BadRequest("No data to insert"); }
            var result = await _productRepository.ProductSearch(_mapper.Map<ProductSearch>(inputModel));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("No data to show."); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}
