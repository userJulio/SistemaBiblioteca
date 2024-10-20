using LibraryRent.Dto.Request;
using LibraryRent.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace LibraryRent.Api.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BooksController: ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(string? nombre, [FromQuery]  PaginationDto pagination)
        {
            var response= await bookService.SearchByNombre(nombre,pagination);
            return response.Succes? Ok(response): BadRequest(response);
        }

        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById(int idlibro)
        {
           
            var response = await bookService.GetBookById(idlibro);
            return response.Succes? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(BookRequestDto requestDto)
        {
            var response= await bookService.AddAsync(requestDto);
            return response.Succes ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, BookRequestDto requestDto)
        {
         
            var response= await bookService.UpdateAsync(id, requestDto);
            return response.Succes? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
           
            var response= await bookService.DeleteAsync(id);
            return response.Succes? Ok(response) : BadRequest(response);
        }


    }
}
