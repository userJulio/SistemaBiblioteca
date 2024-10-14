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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Nombre).HasMaxLength(150);
            builder.Property(x => x.Apellidos).HasMaxLength(200);
            builder.Property(x => x.Dni).HasMaxLength(10);
            builder.ToTable("Clientes", "Library");

        }
    }
}
