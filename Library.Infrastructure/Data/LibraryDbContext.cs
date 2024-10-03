using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<BookWithId> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookWithId>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
