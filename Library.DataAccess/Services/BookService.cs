using AutoMapper;
using Library.Domain.Entities;
using Library.Application.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Library.Shared.DTOs;
using Microsoft.Extensions.Logging;
using Library.Shared.Exceptions;

namespace Library.Application.Services
{
    public class BookService(LibraryDbContext context, IMapper mapper, ILogger<BookService> logger) : IBookService
    {
        public async Task<IEnumerable<BookWithId>> GetAllBooksAsync()
        {
            try
            {
                var books = await context.Books.ToListAsync();
                return books ?? [];
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching books.");
                return [];
            }
        }

        public async Task<BookWithId?> GetBookByIdAsync(int id)
        {
            var book = await context.Books.FindAsync(id);

            return book ?? throw new NotFoundException($"Book with id {id} not found");
        }

        public async Task<BookWithId> AddBookAsync(Book book)
        {
            var bookWithId = mapper.Map<BookWithId>(book);

            context.Books.Add(bookWithId);
            await context.SaveChangesAsync();
            return bookWithId;
        }

        public async Task<BookWithId?> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await context.Books.FindAsync(id);

            if (existingBook == null)
            {
                throw new NotFoundException($"Book with id {id} not found");
            }
            else
            {
                mapper.Map(book, existingBook);

                context.Books.Update(existingBook);
                await context.SaveChangesAsync();

                return existingBook;
            }
        }
    }
}
