using BookHaven.Core.Entities;
using BookHaven.Core.Helpers;

namespace BookHaven.Core.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
        Task<Book> GetBookWithDetailsAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksWithGenresAsync();
        Task<IEnumerable<Book>> GetHighestRatedBooks(int count);
        Task<PaginatedList<Book>> GetPaginatedBooksAsync(int pageIndex, int pageSize, int? genreId = null, string? searchTerm = null);
    }
}
