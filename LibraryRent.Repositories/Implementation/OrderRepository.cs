using LibraryRent.Entities;
using LibraryRent.Repositories.Interface;
using LibrayRent.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Repositories.Implementation
{
    public class OrderRepository : RespositoryBase<Order>, IOrderRepository
    {


        private IDbContextTransaction _transaction { get; set; }
        public OrderRepository(AplicationDbContext context) : base(context)
        {

        }

        public async Task<int> AgregarPedido(Order order)
        {
            await context.AddAsync(order);
            await base.Grabar();
            return order.Id;
        }
        public async Task AgregarDetallePedido( DetailOrder detailOrder)
        {
            await  context.Set<DetailOrder>().AddAsync(detailOrder);
            
        }
        public async Task GrabarDetallePedido()
        {
          
            await base.Grabar();
        }

        public async Task<IDbContextTransaction> CreateTransactionAsync()
        {
            _transaction= await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            return _transaction;
        }

        public async Task<ICollection<Book>> ListarLibrosAlquilosxDni(string Dni)
        {
            var idCliente = await context.Set<Customer>()
                                .Where(x => x.Dni.Trim() == Dni.Trim())
                                .Select(x => x.Id).FirstOrDefaultAsync();

            var IdsPedidosxCliente = await context.Set<Order>()
                                            .Where(x => x.ClienteId == Convert.ToInt32(idCliente))
                                            .AsNoTracking()
                                            .Select(x=>x.Id)
                                            .ToListAsync();

            var listaLibros = await context.Set<DetailOrder>()
                                        .Include(x=>x.Libro)
                                        .Where(x => IdsPedidosxCliente.Contains(x.IdPedido))
                                        .AsNoTracking()
                                        .Select(x=>new Book()
                                        {
                                            Id= x.LibroId,
                                            Nombre=x.Libro.Nombre,
                                            Autor=x.Libro.Autor,
                                            ISBN=x.Libro.ISBN,
                                            Estado=x.Libro.Estado
                                        })
                                        .ToListAsync();
            return listaLibros;
        }
    }
}
