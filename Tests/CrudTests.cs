using Application.Commands.AuthorCommands;
using Application.Commands.BookCommands;
using Application.Handlers.AuthorHandlers;
using Application.Handlers.BookHandlers;
using Application.Interfaces.RepositoryInterfaces;
using Models;
using Moq;

namespace ApplicationTests
{
    public class CrudTests
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IBookRepository> _mockBookRepository;

        [SetUp]
        public void SetUp()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockBookRepository = new Mock<IBookRepository>();
        }

        [Test]
        public async Task AddAuthor_ShouldReturnSuccess()
        {
            // Arrange
            var newAuthor = new Author { Id = 1, Name = "Test Author" };
            _mockAuthorRepository.Setup(repo => repo.AddAuthor(It.IsAny<Author>()))
                .ReturnsAsync(OperationResult<Author>.Success(newAuthor));

            var handler = new AddAuthorCommandHandler(_mockAuthorRepository.Object);
            var command = new AddAuthorCommand("Test Author");

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Test Author", result.Data.Name);
        }

        [Test]
        public async Task UpdateAuthor_ShouldReturnUpdatedAuthor()
        {
            // Arrange
            var updatedAuthor = new Author { Id = 1, Name = "Updated Author" };
            _mockAuthorRepository.Setup(repo => repo.UpdateAuthor(1, It.IsAny<Author>()))
                .ReturnsAsync(OperationResult<Author>.Success(updatedAuthor));

            var handler = new UpdateAuthorCommandHandler(_mockAuthorRepository.Object);
            var command = new UpdateAuthorCommand(1, "Updated Author");

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Updated Author", result.Data.Name);
        }

        [Test]
        public async Task DeleteAuthor_ShouldReturnSuccess()
        {
            // Arrange
            _mockAuthorRepository.Setup(repo => repo.DeleteAuthorById(1))
                .ReturnsAsync(OperationResult<bool>.Success(true));

            var handler = new DeleteAuthorCommandHandler(_mockAuthorRepository.Object);
            var command = new DeleteAuthorCommand(1);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Data);
        }

        [Test]
        public async Task AddBook_ShouldReturnSuccess()
        {
            // Arrange
            var newBook = new Book { Id = 1, Name = "Test Book", Title = "Test Title", AuthorId = 1 };
            _mockBookRepository.Setup(repo => repo.AddBook(It.IsAny<Book>()))
                .ReturnsAsync(OperationResult<Book>.Success(newBook));

            var handler = new AddBookCommandHandler(_mockBookRepository.Object, _mockAuthorRepository.Object);
            var command = new AddBookCommand("Test Book", "Test Title", "Test Description", 1);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Test Book", result.Data.Name);
        }

        [Test]
        public async Task UpdateBook_ShouldReturnUpdatedBook()
        {
            // Arrange
            var updatedBook = new Book { Id = 1, Name = "Updated Book", Title = "Updated Title", AuthorId = 1 };
            _mockBookRepository.Setup(repo => repo.UpdateBook(1, It.IsAny<Book>()))
                .ReturnsAsync(OperationResult<Book>.Success(updatedBook));

            var handler = new UpdateBookCommandHandler(_mockBookRepository.Object);
            var command = new UpdateBookCommand(1, "Updated Book", "Updated Title", "Updated Description", 1);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Updated Book", result.Data.Name);
        }

        [Test]
        public async Task DeleteBook_ShouldReturnSuccess()
        {
            // Arrange
            _mockBookRepository.Setup(repo => repo.DeleteBookById(1))
                .ReturnsAsync(OperationResult<bool>.Success(true));

            var handler = new DeleteBookCommandHandler(_mockBookRepository.Object);
            var command = new DeleteBookCommand(1);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Data);
        }
    }
}
