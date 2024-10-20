using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using LibraryRent.Dto.Request;
using LibraryRent.Dto.Response;
using LibraryRent.Entities;
using LibraryRent.Repositories.Interface;
using LibraryRent.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly ILogger<BookService> logger;
        private readonly IMapper mapper;

        public BookService(IBookRepository bookRepository,
            ILogger<BookService> logger,
            IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async  Task<BaseResponseGeneric<int>> AddAsync(BookRequestDto request)
        {
            var response= new BaseResponseGeneric<int>();
            try
            {
               
                var existeISBN = await bookRepository.ValidarExisteISBN(request.ISBN);
                if (existeISBN)
                {
                    response.ErrorMessage = $"El ISBN: {request.ISBN} ya existe";
                    logger.LogWarning($"{response.ErrorMessage}");
                    return response;
                }
                var bookdb = mapper.Map<Book>(request);
           
                await bookRepository.AddAsync(bookdb);

                response.data = bookdb.Id;
                response.Succes = true;

            }catch(Exception ex) 
            {
                response.ErrorMessage = "Ocurrió un error al registrar";
                logger.LogError($"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var data = await bookRepository.GetAsync(id);
                if(data is null)
                {
                    response.ErrorMessage = $"El registro con id : {id} no existe";
                    logger.LogWarning($"{response.ErrorMessage}");
                    return response;
                }
                await bookRepository.DeleteAsync(id);
                response.Succes = true;

            }
            catch(Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al eliminar ";
                logger.LogError($"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponseGeneric<BookResponseDto>> GetBookById(int idlibro)
        {
            var response= new BaseResponseGeneric<BookResponseDto>();
            try
            {
                var bookdb= await bookRepository.GetAsync(idlibro);
                var data= mapper.Map<BookResponseDto>(bookdb);
                response.data = data;
                response.Succes = true;

            }catch(Exception ex)
            {
                response.ErrorMessage = "Ocurrió un erro al obtener los datos";
                logger.LogError($"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> SearchByNombre(string? nombre, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();

            try
            {
               
                var data = await bookRepository.SearchByNombre(nombre,pagination);

                var bookresponse = mapper.Map<ICollection<BookResponseDto>>(data);

                response.data = bookresponse;
                response.Succes = true;
            }catch(Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener los datos";
                logger.LogError($"{response.ErrorMessage} {ex.Message} ");
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, BookRequestDto request)
        {
           var response= new  BaseResponse();
            try
            {
                var data = await bookRepository.GetAsync(id);
                if(data is null)
                {
                    response.ErrorMessage = $"El registro con el id : {id} no existe";
                    return response;
                }
                var existeISBN = false;
                if(data.ISBN.ToLower().Trim()!= request.ISBN.ToLower().Trim())
                {
                    existeISBN = await bookRepository.ValidarExisteISBN(request.ISBN);
                }
                if (existeISBN)
                {
                    response.ErrorMessage = $"El ISBN: {request.ISBN} ya existe";
                    logger.LogWarning($"{response.ErrorMessage}");
                    return response;
                }
                mapper.Map(request, data);
                await bookRepository.Grabar();
                response.Succes = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al actualizar el registro";
                logger.LogError($"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
    }
}
