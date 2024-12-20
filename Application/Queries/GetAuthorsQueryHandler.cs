

using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
namespace Application.Queries
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, OperationResult<List<Author>>>
    {
        private readonly IAuthorRepository _authorRepository;
        public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), "Author repository cannot be null.");
        }

        public async Task<OperationResult<List<Author>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetAllAuthors();
        }

    }
}
