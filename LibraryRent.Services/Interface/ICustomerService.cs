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
    public interface ICustomerService
    {
        Task<BaseResponseGeneric<ICollection<CustomerResponseDto>>> SearchByNombre(string? nombre,PaginationDto pagination);

        Task<BaseResponseGeneric<CustomerResponseDto>> GetCustomerById(int id);
        Task<BaseResponseGeneric<int>> AddAsync(CustomerRequestDto request);

       Task<BaseResponse> UpdateAsync(int id, CustomerRequestDto request);

        Task<BaseResponse> DeleteAsync(int id);


    }
}
