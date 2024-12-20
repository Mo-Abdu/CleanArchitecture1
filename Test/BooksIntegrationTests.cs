
using Application.Commands.BookCommands;
using Application.Handlers.BookHandlers;
using Application.Interfaces.RepositoryInterfaces;
using FakeItEasy;
using FluentAssertions;
using Models;

namespace IntegrationTest
{
    public class BooksIntegrationTests
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private AddBookCommandHandler _addBookHandler;
        private UpdateBookCommandHandler _updateBookHandler;
        private DeleteBookCommandHandler _deleteBookHandler;

        [SetUp]
        public void SetUp()
        {
            // Mocka repository
            _bookRepository = A.Fake<IBookRepository>();
            _authorRepository = A.Fake<IAuthorRepository>();

            // Skapa handlers och injicera mockade repositoryn
            _addBookHandler = new AddBookCommandHandler(_bookRepository, _authorRepository);
            _updateBookHandler = new UpdateBookCommandHandler(_bookRepository);
            _deleteBookHandler = new DeleteBookCommandHandler(_bookRepository);
        }

        //Add Book Test
       [Test]
        public async Task AddBook_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var command = new AddBookCommand("Book Name", "Book Title", "Book Description", 1);
            var author = new Author { Id = 1, Name = "Author Name" };
            var book = new Book { Id = 1, Name = "Book Name", Title = "Book Title", Description = "Book Description", AuthorId = 1 };

            // Mocka författarrepositoryt för att returnera en framgångsrik operation
            A.CallTo(() => _authorRepository.GetAuthorById(command.AuthorId))
                .Returns(OperationResult<Author>.Success(author));

            // Mocka bokrepositoryt för att returnera en framgångsrik operation
            A.CallTo(() => _bookRepository.AddBook(A<Book>._))
                .Returns(OperationResult<Book>.Success(book));

            // Act
            var result = await _addBookHandler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(book);
        }

        // Get Book by ID Test
        [Test]
        public async Task GetBookById_ShouldReturnBook_WhenValidId()
        {
            // Arrange
            var book = new Book { Id = 1, Name = "Book Name", Title = "Book Title", Description = "Book Description", AuthorId = 1 };

            // Mocka bokrepositoryt för att returnera en bok
            A.CallTo(() => _bookRepository.GetBookById(1))
                .Returns(OperationResult<Book>.Success(book));

            // Act
            var result = await _bookRepository.GetBookById(1);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(book);
        }

        // Update Book Test
        [Test]
        public async Task UpdateBook_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var command = new UpdateBookCommand(1, "Updated Name", "Updated Title", "Updated Description", 1);
            var book = new Book { Id = 1, Name = "Updated Name", Title = "Updated Title", Description = "Updated Description", AuthorId = 1 };

            // Mocka bokrepositoryt för att returnera en framgångsrik uppdatering
            A.CallTo(() => _bookRepository.UpdateBook(1, A<Book>._))
                .Returns(OperationResult<Book>.Success(book));

            // Act
            var result = await _updateBookHandler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(book);
        }

        // Delete Book Test
        [Test]
        public async Task DeleteBook_ShouldReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var command = new DeleteBookCommand(1);
            var book = new Book { Id = 1, Name = "Book Name", Title = "Book Title", Description = "Book Description", AuthorId = 1 };

            // Mocka bokrepositoryt för att returnera en framgångsrik borttagning
            A.CallTo(() => _bookRepository.DeleteBookById(1))
                .Returns(OperationResult<bool>.Success(true));

            // Act
            var result = await _deleteBookHandler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeTrue();
        }

        

    }
}
