using Library.Domain.Entities;
using Library.Shared.Constants;
using Library.Shared.DTOs;

namespace Library.Web.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BookService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

            _httpClient.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, apiKey);
        }

        public async Task<IEnumerable<BookWithId>> GetBooksAsync()
        {
            var books = await _httpClient.GetFromJsonAsync<List<BookWithId>>("/books");
            return books ?? [];
        }

        public async Task<BookWithId?> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookWithId>($"/book/{id}");
        }

        public async Task AddBookAsync(Book book)
        {
            await _httpClient.PostAsJsonAsync("/book", book);
        }

        public async Task UpdateBookAsync(int id, Book book)
        {
            await _httpClient.PutAsJsonAsync($"/book/{id}", book);
        }
    }
}
