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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Nombre).HasMaxLength(150);
            builder.Property(x => x.Autor).HasMaxLength(200);
            builder.Property(x => x.ISBN).HasMaxLength(50);
            builder.ToTable("libros", "Library");
        }
    }
}
