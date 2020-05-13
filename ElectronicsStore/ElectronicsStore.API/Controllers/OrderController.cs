using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, IOrderController
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async ValueTask<ActionResult<OrderOutputModel>> AddOrder(OrderInputModel inputModel)
        {
            if (inputModel == null) { return BadRequest("No data to insert"); }
            var result = await _orderRepository.AddOrder(_mapper.Map<Order>(inputModel));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Operation wasn't executed"); }
                return Ok(_mapper.Map<OrderOutputModel>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}
