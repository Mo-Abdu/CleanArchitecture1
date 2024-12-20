using Application.Interfaces.RepositoryInterfaces;
using Application.Queries;
using Models;
using Moq;

namespace ApplicationTests
{
    public class GetBooksQueryHandlerTests
    {
        private Mock<IBookRepository> _mockBookRepository;
        private GetBooksQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _handler = new GetBooksQueryHandler(_mockBookRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsListOfBooks()
        {
            // Arrange
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1" },
            new Book { Id = 2, Title = "Book 2" }
        };

            _mockBookRepository
                .Setup(repo => repo.GetAllBooks())
                .ReturnsAsync(OperationResult<List<Book>>.Success(books));

            var query = new GetBooksQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(books, result.Data);
        }
    }
}
