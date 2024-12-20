using Application.Commands.AuthorCommands;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
namespace Application.Handlers.AuthorHandlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), "Author repository cannot be null.");
        }

        public async Task<OperationResult<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var newAuthor = new Author
            {
                Name = request.Name
            };

            return await _authorRepository.AddAuthor(newAuthor);

        }
    }
}
