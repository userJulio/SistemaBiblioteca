using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRent.Dto.Request
{
    public class PaginationDto
    {
        private readonly int maxRecordsPerPage = 50;
        public int Page { get; set; } = 1;

        private int recordsPerPage = 10;

        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set
            {
                recordsPerPage = value > maxRecordsPerPage ? maxRecordsPerPage : value;
            }
        }
    }
}
