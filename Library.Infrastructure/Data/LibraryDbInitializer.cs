using Library.Domain.Entities;

namespace Library.Infrastructure.Data
{
    public class LibraryDbInitializer(LibraryDbContext context) : ILibraryDbInitializer
    {
        public void Initialize()
        {
            context.Database.EnsureCreated();

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new BookWithId { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
                    new BookWithId { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
                    new BookWithId { Id = 3, Title = "1984", Author = "George Orwell" }
                );

                context.SaveChanges();
            }
        }
    }
}
