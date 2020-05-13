using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    public interface IOrderController
    {
        ValueTask<ActionResult<OrderOutputModel>> AddOrder(OrderInputModel inputModel);
    }
}