using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Dto.Request
{
    public class CustomerRequestDto
    {
        public string Nombre { get; set; } = default!;
        public string Apellidos { get; set; } = default!;

        [StringLength(10)]
        public string Dni { get; set; } = default!;

        [Range(18, 100)]
        public int Edad { get; set; } = 18;
    }
}
