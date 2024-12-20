using Models;
namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<OperationResult<Book>> GetBookById(int id);
        Task<OperationResult<List<Book>>> GetAllBooks();
        Task<OperationResult<Book>> AddBook(Book book);
        Task<OperationResult<bool>> DeleteBookById(int id);
        Task<OperationResult<Book>> UpdateBook(int id, Book book);

    }
}
