using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using LibraryRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Repositories.Interface
{
    public interface ICustomerRepository: IRepositoryBase<Customer>
    {
        Task<ICollection<Customer>> SearchByNombre(string? nombre, PaginationDto pagination);

        Task<Customer?> GetCustomerByDni(string Dni);

      
    }
}
