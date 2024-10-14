using LibraryRent.Dto.Request;
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
    public class BookRepository: RespositoryBase<Book>, IBookRepository
    {
        private readonly IHttpContextAccessor contexto;

        public BookRepository(AplicationDbContext context, IHttpContextAccessor contexto) :base(context)
        {
            this.contexto = contexto;
        }

        public async Task<ICollection<Book>> SearchByNombre(string? nombre, PaginationDto pagination)
        {

            var nombreSearch = nombre is null ? "" : nombre;

            var queryable =  context.Set<Book>()
                .Where(x => x.Nombre.ToLower().Trim().Contains(nombreSearch.ToLower().Trim()))
                .AsNoTracking()
                .AsQueryable();

            await contexto.HttpContext.InsertPaginationHeader(queryable);
            var lista = await queryable.Paginate(pagination).ToListAsync();
            return lista;
        }

        public async Task<bool> ValidarExisteISBN(string isbn)
        {
            var libro= await context.Set<Book>()
                                .Where(x=>x.ISBN.ToLower().Trim()==isbn.ToLower().Trim())
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            return libro is null ? false : true;
        }

        public async Task<int> GetIdByISBN(string isbn)
        {
            var idlibro = await context.Set<Book>()
                          .Where(x => x.ISBN.ToLower().Trim() == isbn.ToLower().Trim())
                          .AsNoTracking()
                          .Select(x => x.Id)
                          .FirstOrDefaultAsync();
            return idlibro;

        }

    }
}
