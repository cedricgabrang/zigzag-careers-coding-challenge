using Library.Domain.Entities;
using Library.Shared.DTOs;

namespace Library.Web.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookWithId>> GetBooksAsync();
        Task<BookWithId?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(int id, Book book);
    }
}
