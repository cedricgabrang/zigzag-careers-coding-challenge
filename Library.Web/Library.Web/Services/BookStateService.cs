using Library.Domain.Entities;

namespace Library.Web.Services
{
    public class BookStateService
    {
        public BookWithId CurrentBook { get; private set; }

        public void SetBook(BookWithId book)
        {
            CurrentBook = book;
        }
    }

}
