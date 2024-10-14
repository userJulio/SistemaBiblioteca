using AutoMapper;
using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using LibraryRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Services.Profiles
{
    public class CustomerProfile :Profile
    {
        public CustomerProfile()
        {
            //origen > destino
            CreateMap<CustomerRequestDto, Customer>();
            CreateMap<Customer, CustomerResponseDto>();
        }

    }
}
