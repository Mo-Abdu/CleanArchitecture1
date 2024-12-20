using Application.Interfaces.RepositoryInterfaces;
using Application.Queries;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests
{
    public class GetBookByIdQueryHandlerTests
    {
        private Mock<IBookRepository> _mockBookRepository;
        private GetBookByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _handler = new GetBookByIdQueryHandler(_mockBookRepository.Object);
        }

        [Test]
        public async Task Handle_ExistingBookId_ReturnsBook()
        {
            // Arrange
            int bookId = 1;
            var expectedBook = new Book { Id = bookId, Title = "Test Book" };
            _mockBookRepository
                .Setup(repo => repo.GetBookById(bookId))
                .ReturnsAsync(OperationResult<Book>.Success(expectedBook));

            var query = new GetBookByIdQuery(bookId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(expectedBook, result.Data);
        }

        [Test]
        public async Task Handle_NonExistingBookId_ReturnsFailure()
        {
            // Arrange
            int bookId = 99;
            _mockBookRepository
                .Setup(repo => repo.GetBookById(bookId))
                .ReturnsAsync(OperationResult<Book>.Failure("Book not found."));

            var query = new GetBookByIdQuery(bookId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Book not found.", result.ErrorMessage);
        }
    }
}
