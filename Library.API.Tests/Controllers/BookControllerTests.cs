using FluentAssertions;
using Library.API.Controllers;
using Library.API.Tests.Base;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Shared.DTOs;
using Library.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.API.Tests.Controllers
{
    public class BookControllerTests : UnitTestBase<BookController>
    {
        private readonly Mock<IBookService> _bookServiceMock;

        public BookControllerTests()
        {
            _bookServiceMock = Mocker.GetMock<IBookService>();
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnOk_WhenBooksExist()
        {
            //Arrange
            var books = new List<BookWithId>
            {
                new() { Id = 1, Title = "Book 1" },
                new() { Id = 2, Title = "Book 2" }
            };

            _bookServiceMock
               .Setup(x => x.GetAllBooksAsync())
               .ReturnsAsync(books);

            //Act
            var result = await Sut.GetAllBooks();

            //Assert
            _bookServiceMock
                .Verify(x => x.GetAllBooksAsync(), Times.Once());

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(books);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnOkWithEmptyList_WhenNoBooksExist()
        {
            //Arrange
            var books = Enumerable.Empty<BookWithId>();

            _bookServiceMock
               .Setup(x => x.GetAllBooksAsync())
               .ReturnsAsync(books);

            //Act
            var result = await Sut.GetAllBooks();

            //Assert
            _bookServiceMock
                .Verify(x => x.GetAllBooksAsync(), Times.Once());

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedBooks = okResult.Value.Should().BeAssignableTo<IEnumerable<BookWithId>>().Subject;
            returnedBooks.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBookById_ShouldReturnOk_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            var expectedBook = new BookWithId { Id = bookId, Title = "Test Book", Author = "Test Author" };

            _bookServiceMock
                .Setup(x => x.GetBookByIdAsync(bookId))
                .ReturnsAsync(expectedBook);

            // Act
            var result = await Sut.GetBookById(bookId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult?.Value.Should().BeEquivalentTo(expectedBook);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 999; // Assuming this ID does not exist

            _bookServiceMock.Setup(service => service.GetBookByIdAsync(bookId))
                .ThrowsAsync(new NotFoundException($"Book with id {bookId} not found"));

            // Act
            var result = await Sut.GetBookById(bookId);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task AddBook_ShouldReturnCreated_WhenBookIsAddedSuccessfully()
        {
            // Arrange
            var newBook = new Book { Title = "New Book" };
            var createdBook = new BookWithId { Id = 1, Title = "New Book" };

            _bookServiceMock
                .Setup(x => x.AddBookAsync(newBook))
                .ReturnsAsync(createdBook);

            // Act
            var result = await Sut.AddBook(newBook);

            // Assert
            var createdResult = result.Should().BeOfType<OkObjectResult>().Subject;
            createdResult.Value.Should().BeEquivalentTo(createdBook);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnOk_WhenBookIsUpdatedSuccessfully()
        {
            // Arrange
            var bookId = 1;
            var updatedBook = new Book { Title = "Updated Book" };
            var resultBook = new BookWithId { Id = bookId, Title = "Updated Book" };

            _bookServiceMock
                .Setup(x => x.UpdateBookAsync(bookId, updatedBook))
                .ReturnsAsync(resultBook);

            // Act
            var result = await Sut.UpdateBook(bookId, updatedBook);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult?.Value.Should().BeEquivalentTo(updatedBook);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 999; // Assuming this ID does not exist
            var bookToUpdate = new Book { Title = "Updated Title", Author = "Updated Author" };

            _bookServiceMock
                .Setup(service => service.UpdateBookAsync(bookId, bookToUpdate))
                .ThrowsAsync(new NotFoundException($"Book with id {bookId} not found"));

            // Act
            var result = await Sut.UpdateBook(bookId, bookToUpdate);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}