using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Shared.DTOs;
using Library.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.API.Controllers
{
    [Tags("LibraryAPI")]
    [Route("[controller]")]
    [ApiController]
    public class BookController(IBookService bookService) : ControllerBase
    {
        /// <summary>
        /// Get all books
        /// </summary>
        /// <remarks>
        /// Retrieve a list of all books in the library.
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("~/books")]
        [ProducesResponseType(typeof(BookWithId[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <remarks>
        /// Add a new book to the library.
        /// </remarks>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookWithId), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddBook([FromBody, SwaggerRequestBody(Required = true)] Book book)
        {
            var createdBook = await bookService.AddBookAsync(book);
            return Ok(createdBook);
        }

        /// <summary>
        /// Get a specific book
        /// </summary>
        /// <remarks>
        /// Retrieve a specific book by its ID.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookWithId), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Update a book
        /// </summary>
        /// <remarks>
        /// Update an existing book by its ID.
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="book"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BookWithId), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            try
            {
                var updatedBook = await bookService.UpdateBookAsync(id, book);

                return Ok(updatedBook);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
