using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Services.Interface
{
    public interface IBookService
    {

        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> SearchByNombre(string? nombre,PaginationDto pagination);

        Task<BaseResponseGeneric<BookResponseDto>> GetBookById(int idlibro);
        Task<BaseResponseGeneric<int>> AddAsync(BookRequestDto request);

        Task<BaseResponse> UpdateAsync(int id, BookRequestDto request);

        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetLibroByIsbn(string? isbn);

    }
}
