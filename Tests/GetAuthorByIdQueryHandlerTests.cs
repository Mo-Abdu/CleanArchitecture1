using Application.Interfaces.RepositoryInterfaces;
using Application.Queries;
using Models;
using Moq;
namespace ApplicationTests
{
    public class GetAuthorByIdQueryHandlerTests
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private GetAuthorByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _handler = new GetAuthorByIdQueryHandler(_mockAuthorRepository.Object);
        }

        [Test]
        public async Task Handle_ExistingAuthorId_ReturnsAuthor()
        {
            // Arrange
            int authorId = 1;
            var expectedAuthor = new Author { Id = authorId, Name = "Test Author" };
            _mockAuthorRepository
                .Setup(repo => repo.GetAuthorById(authorId))
                .ReturnsAsync(OperationResult<Author>.Success(expectedAuthor));

            var query = new GetAuthorByIdQuery(authorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(expectedAuthor, result.Data);
        }

        [Test]
        public async Task Handle_NonExistingAuthorId_ReturnsFailure()
        {
            // Arrange
            int authorId = 99;
            _mockAuthorRepository
                .Setup(repo => repo.GetAuthorById(authorId))
                .ReturnsAsync(OperationResult<Author>.Failure("Author not found."));

            var query = new GetAuthorByIdQuery(authorId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Author not found.", result.ErrorMessage);
        }
    }
}
