using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using LibraryRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Services.Interface
{
    public interface IOrderService
    {

        Task<BaseResponseGeneric<int>> AgregarPedido(OrderRequestDto request);

        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> ListarLibrosAlquilados(string Dni);

    }
}
