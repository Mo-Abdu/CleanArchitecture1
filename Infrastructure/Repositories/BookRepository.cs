using Application.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RealDatabase _context;

        public BookRepository(RealDatabase context)
        {
            _context = context;
        }

        public async Task<OperationResult<Book>> GetBookById(int id)
        {
            try
            {
                var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
                return book != null
                    ? OperationResult<Book>.Success(book)
                    : OperationResult<Book>.Failure("Book not found.");
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure($"Error retrieving book: {ex.Message}");
            }
        }

        public async Task<OperationResult<List<Book>>> GetAllBooks()
        {
            try
            {
                var books = await _context.Books.Include(b => b.Author).ToListAsync();
                return books.Any()
                    ? OperationResult<List<Book>>.Success(books)
                    : OperationResult<List<Book>>.Failure("No books found.");
            }
            catch (Exception ex)
            {
                return OperationResult<List<Book>>.Failure($"Error retrieving books: {ex.Message}");
            }
        }

        public async Task<OperationResult<Book>> AddBook(Book book)
        {
            try
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                return OperationResult<Book>.Success(book);
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure($"Error adding book: {ex.Message}");
            }
        }

        public async Task<OperationResult<Book>> UpdateBook(int id, Book book)
        {
            try
            {
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook == null)
                    return OperationResult<Book>.Failure("Book not found.");

                existingBook.Name = book.Name;
                existingBook.Title = book.Title;
                existingBook.Description = book.Description;
                existingBook.AuthorId = book.AuthorId;

                await _context.SaveChangesAsync();
                return OperationResult<Book>.Success(existingBook);
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure($"Error updating book: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> DeleteBookById(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return OperationResult<bool>.Failure("Book not found.");

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"Error deleting book: {ex.Message}");
            }
        }
    }
}
