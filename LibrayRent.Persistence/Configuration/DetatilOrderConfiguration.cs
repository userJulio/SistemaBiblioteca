using LibraryRent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrayRent.Persistence.Configuration
{
    public class DetatilOrderConfiguration : IEntityTypeConfiguration<DetailOrder>
    {
        public void Configure(EntityTypeBuilder<DetailOrder> builder)
        {

            builder.ToTable("DetallePedido", "Library");
        }
    }
}
