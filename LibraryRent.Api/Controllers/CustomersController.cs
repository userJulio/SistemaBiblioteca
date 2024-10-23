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

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int idcustomer)
        {
           
            var response = await customerService.GetCustomerById(idcustomer);
            return response.Succes? Ok(response) : BadRequest(Response);
        }

        [HttpPost("GuardarCliente")]
        public async Task<IActionResult> Agregar(CustomerRequestDto requestDto)
        {
            var response = await customerService.AddAsync(requestDto);
            return response.Succes ? Ok(response) : BadRequest(response);

        }

        [HttpPut("updateCustomer")]
        public async Task<IActionResult> Actualizar(int idCliente,CustomerRequestDto requestDto)
        {
            var response = await customerService.UpdateAsync(idCliente, requestDto);
            return response.Succes? Ok(response) : BadRequest(response);

        }

        [HttpDelete("deleteCustomer")]
        public async Task<IActionResult> Eliminar(int idCliente)
        {
          
            var response = await customerService.DeleteAsync(idCliente);
            return response.Succes ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetClienteByDni")]
        public async Task<IActionResult> GetClienteByDni(string Dni)
        {
            var response = await customerService.GetClienteByDni(Dni);
            return response.Succes ? Ok(response) :BadRequest(response);
        }

    }
}
