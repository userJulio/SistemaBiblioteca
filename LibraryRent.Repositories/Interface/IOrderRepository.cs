using LibraryRent.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Repositories.Interface
{
    public interface IOrderRepository: IRepositoryBase<Order>
    {
        Task<int> AgregarPedido(Order order);

        Task AgregarDetallePedido(DetailOrder detailOrder);

        Task GrabarDetallePedido();

        Task<IDbContextTransaction> CreateTransactionAsync();

        //Lista de libros alquilados
        Task<ICollection<Book>> ListarLibrosAlquilosxDni(string Dni);
    }
}
