using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Entities
{
    public class DetailOrder
    {
        [Key]
        public int Id { get; set; }

        public  int IdPedido { get; set; }

        public int LibroId { get; set; }

        [ForeignKey("LibroId")]
        public virtual Book Libro { get; set; } = default!;

        [ForeignKey("IdPedido")]
        public virtual Order Pedido { get; set; } = default!;

    }
}
