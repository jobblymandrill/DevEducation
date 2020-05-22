using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    public interface IProductController
    {
        ValueTask<ActionResult<List<ProductOutputModel>>> ProductSearch(ProductSearchInputModel inputModel);
    }
}