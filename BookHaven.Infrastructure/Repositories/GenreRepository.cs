using BookHaven.Core.Contracts;
using BookHaven.Core.Entities;
using BookHaven.Infrastructure.Data;

namespace BookHaven.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
