using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using LibraryRent.Entities;
using LibraryRent.Repositories.Interface;
using LibraryRent.Repositories.Utils;
using LibrayRent.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Repositories.Implementation
{
    public class CustomerRepository: RespositoryBase<Customer> ,ICustomerRepository
    {
        private readonly IHttpContextAccessor contexto;

        public CustomerRepository(AplicationDbContext context,IHttpContextAccessor contexto) : base(context)
        {
            this.contexto = contexto;
        }

        public async Task<Customer?> GetCustomerByDni(string Dni)
        {
            
            var cliente = await context.Set<Customer>()
                .Where(x => x.Dni.ToLower().Trim() == Dni.ToLower().Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return cliente;
        }

        public async Task<ICollection<Customer>> SearchByNombre(string? nombre, PaginationDto pagination)
        {
            var searchnombre = nombre is null ? "" : nombre;
            var queryable = context.Set<Customer>()
                .OrderBy(x=>x.Id)
                .Where(x => x.Nombre.ToLower().Trim().Contains(searchnombre.ToLower().Trim()))
               .AsNoTracking()
               .AsQueryable();

            await contexto.HttpContext.InsertPaginationHeader(queryable);
            var lista = await queryable.Paginate(pagination).ToListAsync();
            return lista;

        }

        public async Task<ICollection<Customer>> GetListCustomerByDni(string? Dni)
        {
            var dnisearch = (Dni is null) ? "" : Dni;
            var lista = await context.Set<Customer>()
                        .Where(x => x.Dni.ToLower().Trim().Contains(dnisearch.ToLower().Trim()))
                        .AsNoTracking()
                        .ToListAsync();
            return lista;
        }

      

    }
}
