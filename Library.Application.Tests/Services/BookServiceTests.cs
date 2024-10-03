using AutoMapper;
using FluentAssertions;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Shared.DTOs;
using Library.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Library.Application.Tests.Services
{
    public class BookServiceTests : IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly BookService _bookService;
        private readonly Mock<ILogger<BookService>> _loggerMock;
        private readonly IMapper _mapper;

        public BookServiceTests()
        {
            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            _context = new LibraryDbContext(options);

            // Set up the logger mock
            _loggerMock = new Mock<ILogger<BookService>>();

            // Set up AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Book, BookWithId>();
            });
            _mapper = mappingConfig.CreateMapper();

            // Create the BookService instance
            _bookService = new BookService(_context, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnAllBooks_WhenBooksExist()
        {
            //Arrange
            _context.Books.Add(new BookWithId { Id = 1, Title = "Book 1", Author = "Author 1" });
            _context.Books.Add(new BookWithId { Id = 2, Title = "Book 2", Author = "Author 2" });
            await _context.SaveChangesAsync();

            //Act
            var result = await _bookService.GetAllBooksAsync();

            //Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnEmptyList_WhenNoBooksExist()
        {
            //Act
            var result = await _bookService.GetAllBooksAsync();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var book = new BookWithId { Id = 1, Title = "Test Book", Author = "Test Author" };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Act
            var result = await _bookService.GetBookByIdAsync(book.Id);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldThrowNotFoundException_WhenBookDoesNotExist()
        {
            // Act
            Func<Task> act = async () => await _bookService.GetBookByIdAsync(999);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AddBookAsync_ShouldAddBook_WhenBookIsValid()
        {
            // Arrange
            var book = new Book { Title = "New Book", Author = "New Author" };

            // Act
            var result = await _bookService.AddBookAsync(book);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);

            var addedBook = await _context.Books.FindAsync(result.Id);
            addedBook.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldUpdateBook_WhenBookExists()
        {
            // Arrange
            var existingBook = new BookWithId { Id = 1, Title = "Original Title", Author = "Original Author" };
            _context.Books.Add(existingBook);
            await _context.SaveChangesAsync();

            var updatedBook = new Book { Title = "Updated Title", Author = "Updated Author" };

            // Act
            var result = await _bookService.UpdateBookAsync(existingBook.Id, updatedBook);

            // Assert
            result.Should().NotBeNull();
            result?.Title.Should().Be(updatedBook.Title);
            result?.Author.Should().Be(updatedBook.Author);

            var updatedBookInContext = await _context.Books.FindAsync(existingBook.Id);
            updatedBookInContext.Should().NotBeNull();
            updatedBookInContext?.Title.Should().Be(updatedBook.Title);
            updatedBookInContext?.Author.Should().Be(updatedBook.Author);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldThrowNotFoundException_WhenBookDoesNotExist()
        {
            // Arrange
            var updatedBook = new Book { Title = "Updated Title", Author = "Updated Author" };

            // Act
            Func<Task> act = async () => await _bookService.UpdateBookAsync(999, updatedBook); // Assuming 999 does not exist

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Book with id 999 not found");
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Clean up the in-memory database
            _context.Dispose(); // Dispose of the context
        }
    }

}
