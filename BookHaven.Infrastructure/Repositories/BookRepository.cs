using BookHaven.Core.Contracts;
using BookHaven.Core.Entities;
using BookHaven.Core.Helpers;
using BookHaven.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithGenresAsync()
        {
            return await _context.Books
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _context.Books
                .Where(b => b.GenreId == genreId)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Book> GetBookWithDetailsAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetHighestRatedBooks(int count)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews)
                .OrderByDescending(b => b.Reviews.Any() ? b.Reviews.Average(r => r.Rating) : 0)
                .ThenByDescending(b => b.Reviews.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<PaginatedList<Book>> GetPaginatedBooksAsync(int pageIndex, int pageSize, int? genreId = null, string? searchTerm = null)
        {
            var query = _context.Books
                .Include(b => b.Genre)
                .AsQueryable();

            // Genre
            if (genreId.HasValue)
            {
                query = query.Where(b => b.GenreId == genreId.Value);
            }

            // Search
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTermLower = searchTerm.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(searchTermLower) ||
                    b.Author.ToLower().Contains(searchTermLower));
            }

            return await PaginatedList<Book>.CreateAsync(query, pageIndex, pageSize);
        }
    }
}
