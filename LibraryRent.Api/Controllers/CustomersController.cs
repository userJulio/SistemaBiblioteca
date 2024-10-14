using LibraryRent.Dto.Request;
using LibraryRent.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryRent.Api.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomersController:ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(string? nombre,[FromQuery] PaginationDto pagination)
        {
            var response= await customerService.SearchByNombre(nombre,pagination);
            return response.Succes? Ok(response) : BadRequest(response);
        }

        [HttpPost("GuardarCliente")]
        public async Task<IActionResult> Agregar(CustomerRequestDto requestDto)
        {
            var response = await customerService.AddAsync(requestDto);
            return response.Succes ? Ok(response) : BadRequest(response);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id,CustomerRequestDto requestDto)
        {
            var response = await customerService.UpdateAsync(id, requestDto);
            return response.Succes? Ok(response) : BadRequest(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = await customerService.DeleteAsync(id);
            return response.Succes ? Ok(response) : BadRequest(response);
        }

        [HttpPost("RegistrarPedido")]
        public async Task<IActionResult> RegistrarPedido(IEnumerable<CustomerRequestDto> ListaClientes)
        {
            return Ok();
        }

    }
}
