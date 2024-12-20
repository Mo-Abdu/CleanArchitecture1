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
    public class GetAuthorsQueryHandlerTests
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private GetAuthorsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _handler = new GetAuthorsQueryHandler(_mockAuthorRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsListOfAuthors()
        {
            // Arrange
            var authors = new List<Author>
        {
            new Author { Id = 1, Name = "Author 1" },
            new Author { Id = 2, Name = "Author 2" }
        };

            _mockAuthorRepository
                .Setup(repo => repo.GetAllAuthors())
                .ReturnsAsync(OperationResult<List<Author>>.Success(authors));

            var query = new GetAuthorsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(authors, result.Data);
        } 
    }

}
