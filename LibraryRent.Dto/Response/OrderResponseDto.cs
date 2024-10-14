using LibraryRent.Dto.Request;
using LibraryRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Dto.Response
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public DateTime FechaPedido { get; set; }

        public int ClienteId { get; set; }

        public IEnumerable<Book> Libros { get; set; } = default!;

        public bool Estado { get; set; }
    }
}
