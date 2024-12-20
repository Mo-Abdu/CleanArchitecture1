using Application.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly RealDatabase _context;

        public AuthorRepository(RealDatabase context)
        {
            _context = context;
        }

        public async Task<OperationResult<List<Author>>> GetAllAuthors()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();
                return authors.Any()
                    ? OperationResult<List<Author>>.Success(authors)
                    : OperationResult<List<Author>>.Failure("No authors found.");
            }
            catch (Exception ex)
            {
                return OperationResult<List<Author>>.Failure($"Error retrieving authors: {ex.Message}");
            }
        }

        public async Task<OperationResult<Author>> GetAuthorById(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                return author != null
                    ? OperationResult<Author>.Success(author)
                    : OperationResult<Author>.Failure($"Author with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure($"Error retrieving author: {ex.Message}");
            }
        }

        public async Task<OperationResult<Author>> AddAuthor(Author author)
        {
            try
            {
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();
                return OperationResult<Author>.Success(author);
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure($"Error adding author: {ex.Message}");
            }
        }

        public async Task<OperationResult<Author>> UpdateAuthor(int id, Author author)
        {
            try
            {
                var existingAuthor = await _context.Authors.FindAsync(id);
                if (existingAuthor == null)
                    return OperationResult<Author>.Failure($"Author with ID {id} not found.");

                existingAuthor.Name = author.Name;
                await _context.SaveChangesAsync();
                return OperationResult<Author>.Success(existingAuthor);
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure($"Error updating author: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> DeleteAuthorById(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                    return OperationResult<bool>.Failure($"Author with ID {id} not found.");

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"Error deleting author: {ex.Message}");
            }
        }
    }
}
