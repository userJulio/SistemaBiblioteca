using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaPedido { get; set; }

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Customer Cliente { get; set; } = default!;

        public bool Estado { get; set; }


    }
}
