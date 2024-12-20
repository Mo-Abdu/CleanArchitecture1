
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Models;
namespace Application.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), "Book repository cannot be null.");
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {

            return await _bookRepository.GetBookById(request.Id);
        }
    }

}
