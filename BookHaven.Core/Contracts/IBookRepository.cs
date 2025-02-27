using BookHaven.Core.Entities;

namespace BookHaven.Core.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
        Task<Book> GetBookWithDetailsAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksWithGenresAsync();
    }
}
