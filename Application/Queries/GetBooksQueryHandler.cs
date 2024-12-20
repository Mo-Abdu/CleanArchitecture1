
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
namespace Application.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, OperationResult<List<Book>>>
    {
        private readonly IBookRepository _bookRepository;


        public GetBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), "Book repository cannot be null.");
        }

        public async Task<OperationResult<List<Book>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAllBooks();

        }

    }
}