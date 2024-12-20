using Application.Commands.BookCommands;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
namespace Application.Handlers.BookHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public AddBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), "Book repository cannot be null.");
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), "Author repository cannot be null.");
        }

        public async Task<OperationResult<Book>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = new Book
            {
                Name = request.Name,
                Title = request.Title,
                Description = request.Description,
                AuthorId = request.AuthorId
            };

            return await _bookRepository.AddBook(newBook);
        }
    }
}
