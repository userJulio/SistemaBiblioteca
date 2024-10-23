using LibraryRent.Dto.Request;
using LibraryRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Repositories.Interface
{
    public interface IBookRepository: IRepositoryBase<Book>
    {
        Task<ICollection<Book>> SearchByNombre(string? nombre, PaginationDto pagination);
        Task<bool> ValidarExisteISBN(string isbn);

        Task<int> GetIdByISBN(string isbn);
        Task<ICollection<Book>> GetLibroByIsbn(string? isbn);
    }
}
