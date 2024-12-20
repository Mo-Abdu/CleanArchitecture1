using Application.Commands.AuthorCommands;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
namespace Application.Handlers.AuthorHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), "Author repository cannot be null.");
        }

        public async Task<OperationResult<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author { Name = request.Name };
            return await _authorRepository.UpdateAuthor(request.Id, author);
        }
    }

    
}
