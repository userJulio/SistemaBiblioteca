using LibraryRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Dto.Request
{
    public class OrderRequestDto
    {
        public string FechaPedido { get; set; } = default!;

        public string HoraPedido { get; set; } = default!;

        public CustomerRequestDto Cliente { get; set; } = default!;

        public ICollection<BookRequestDto> Libros { get; set; } = default!;

        public bool Estado { get; set; }
    }
}
