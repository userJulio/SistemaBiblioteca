using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Dto.Response
{
    public class BaseResponse
    {
        public bool Succes { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
