﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Entities
{
    public class Book 
    {
        [Key]
        public int Id { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Autor { get; set; } = default!;
        public string ISBN { get; set; }= default!;

        public  bool Estado { get; set; }
    }
}
