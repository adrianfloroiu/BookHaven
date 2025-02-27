using BookHaven.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHaven.Core.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
        Task<Book> GetBookWithDetailsAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksWithGenresAsync();
    }
}
