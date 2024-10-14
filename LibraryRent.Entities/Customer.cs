using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string Dni { get; set; } = default!;

        [Range(18,100)]
        public int Edad { get; set; }
    }
}
