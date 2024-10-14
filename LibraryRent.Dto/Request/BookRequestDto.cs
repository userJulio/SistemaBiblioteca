using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Dto.Request
{
    public class BookRequestDto
    {
        public string Nombre { get; set; } = default!;
        public string Autor { get; set; } = default!;
        public string ISBN { get; set; } = default!;

        public bool Estado { get; set; }
    }
}
