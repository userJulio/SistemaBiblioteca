using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Dto.Response
{
    public class CustomerResponseDto
    {
        public int Id { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string Dni { get; set; } = default!;

        public int Edad { get; set; }
    }
}
