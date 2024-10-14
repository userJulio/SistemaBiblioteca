using AutoMapper;
using Azure.Core;
using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using LibraryRent.Entities;
using LibraryRent.Repositories.Interface;
using LibraryRent.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<CustomerService> logger;
        private readonly IMapper mapper;


        public CustomerService(ICustomerRepository customerRepository,
            ILogger<CustomerService> logger,
            IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<int>> AddAsync(CustomerRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var cliente= await customerRepository.GetCustomerByDni(request.Dni);
                var existeDni = cliente is null ? false : true;
                if (existeDni)
                {
                    response.ErrorMessage = "El Dni ya existe";
                    logger.LogWarning($"{response.ErrorMessage}");
                    return response;
                }
                var customerdb = mapper.Map<Customer>(request);
                await  customerRepository.AddAsync(customerdb);
                var idCustomer = customerdb.Id;
                response.data = idCustomer;
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al registrar el Cliente";
                logger.LogError($" {response.ErrorMessage} {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var customerdb = await customerRepository.GetAsync(id);
                if (customerdb is null)
                {
                    response.ErrorMessage = $"El registro con el id :{id} no existe";
                    return response;
                }
                await customerRepository.DeleteAsync(id);
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al eliminar el registro";
                logger.LogError($" {response.ErrorMessage}  {ex.Message}");

            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<CustomerResponseDto>>> SearchByNombre(string? nombre, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<CustomerResponseDto>>();
            try
            {
                var data = await customerRepository.SearchByNombre(nombre,pagination);
                response.data = mapper.Map<ICollection<CustomerResponseDto>>(data);
                response.Succes = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError($"{response.ErrorMessage}  {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, CustomerRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var customerdb = await customerRepository.GetAsync(id);
                if (customerdb is null)
                {
                    response.ErrorMessage = $"Registro con id {id} no encontrado";
                    return response;
                }
                var existeCliente = false;
                if (customerdb.Dni.Trim() != request.Dni.Trim())
                {
                    var cliente = await customerRepository.GetCustomerByDni(request.Dni);
                    existeCliente = cliente is null ? false : true;
                }

                if (existeCliente)
                {
                    response.ErrorMessage = "El Dni del cliente ya existe";
                    logger.LogWarning($"{response.ErrorMessage}");
                    return response;
                }
                mapper.Map(request, customerdb);
                await customerRepository.Grabar();
                response.Succes = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar los datos";
                logger.LogError($"{response.ErrorMessage} {ex.Message}");
            }
            return response;

        }
    }
}
