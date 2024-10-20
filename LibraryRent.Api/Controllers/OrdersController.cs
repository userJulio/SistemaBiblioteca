using LibraryRent.Dto.Request;
using LibraryRent.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryRent.Api.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("RegistrarPedido")]
        public async Task<IActionResult> AgregarPedido(OrderRequestDto orderRequest)
        {
            var response = await orderService.AgregarPedido(orderRequest);
            return response.Succes? Ok(response): BadRequest(response);
        }

        [HttpGet("ReporteLibrosAlquiladorByDni")]
        public async Task<IActionResult> ListarLibrosAlquiladosXDni(string Dni)
        {
            var response = await orderService.ListarLibrosAlquilados(Dni);
            return response.Succes? Ok(response): BadRequest(response);
        }

    }
}
