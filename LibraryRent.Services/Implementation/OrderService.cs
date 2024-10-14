using AutoMapper;
using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using LibraryRent.Entities;
using LibraryRent.Repositories.Interface;
using LibraryRent.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IBookRepository bookRepository;
        private readonly ILogger<OrderService> logger;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IBookRepository bookRepository,
            ILogger<OrderService> logger,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.bookRepository = bookRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public  async Task<BaseResponseGeneric<int>> AgregarPedido(OrderRequestDto request)
        {
            var response =new BaseResponseGeneric<int>();

            using (var transaction = await orderRepository.CreateTransactionAsync())
            {
                try
                {
                    var order = new Order();
                    var cliente = await customerRepository.GetCustomerByDni(request.Cliente.Dni);
                    var nuevoCliente = new Customer();
                    if (cliente is null)
                    {
                        nuevoCliente.Nombre = request.Cliente.Nombre;
                        nuevoCliente.Apellidos = request.Cliente.Apellidos;
                        nuevoCliente.Dni = request.Cliente.Dni;
                        nuevoCliente.Edad = request.Cliente.Edad;
                        await customerRepository.AddAsync(nuevoCliente);
                    }

                    order.ClienteId = cliente is null ? nuevoCliente.Id : cliente.Id;

                    var fechaPedidoyHora = $"{request.FechaPedido} {request.HoraPedido}";

                    DateTime dateFechaPedido = DateTime.Now;
                    var culture = CultureInfo.CreateSpecificCulture("es-PE");
                    var styles = DateTimeStyles.None;


                    bool fechaValida = DateTime.TryParse(fechaPedidoyHora, culture, styles, out dateFechaPedido);
                    if (!fechaValida)
                    {
                        throw new Exception($"La fecha del pedido no es válida");

                    }
                    order.FechaPedido = dateFechaPedido;
           
                    order.Estado = true;
                    await orderRepository.AgregarPedido(order);

                    foreach (var item in request.Libros)
                    {
                        var orderDetalle = new DetailOrder();

                        orderDetalle.LibroId = await bookRepository.GetIdByISBN(item.ISBN);
                        orderDetalle.IdPedido = order.Id;
                        await orderRepository.AgregarDetallePedido(orderDetalle);
                    }
                   
                    await orderRepository.GrabarDetallePedido();
                    await transaction.CommitAsync();
                    response.data = order.Id;
                    response.Succes = true;
                }
                catch (Exception ex)
                {
                   await transaction.RollbackAsync();
                    response.ErrorMessage = "Ocurrio un error al registrar el pedido";
                    logger.LogError($" {response.ErrorMessage} {ex.Message}");
                }
            }
                return response;
        }

        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> ListarLibrosAlquilados(string Dni)
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                var listalibros = await orderRepository.ListarLibrosAlquilosxDni(Dni);
                var librosresponse= mapper.Map<ICollection<BookResponseDto>>(listalibros);
                response.data = librosresponse;
                response.Succes = true;

            }
            catch(Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener los datos";
                logger.LogError($"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
    }
}
