using Library.Domain.Entities;
using Library.Shared.DTOs;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookWithId?> GetBookByIdAsync(int id);
        Task<IEnumerable<BookWithId>> GetAllBooksAsync();
        Task<BookWithId> AddBookAsync(Book book);
        Task<BookWithId?> UpdateBookAsync(int id, Book book);
    }
}
