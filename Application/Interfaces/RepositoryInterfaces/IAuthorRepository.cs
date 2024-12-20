using Models;
namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<OperationResult<List<Author>>> GetAllAuthors();
        Task<OperationResult<Author>> GetAuthorById(int id);
        Task<OperationResult<Author>> AddAuthor(Author author);
        Task<OperationResult<Author>> UpdateAuthor(int id, Author author);
        Task<OperationResult<bool>> DeleteAuthorById(int id);

    }
}
 