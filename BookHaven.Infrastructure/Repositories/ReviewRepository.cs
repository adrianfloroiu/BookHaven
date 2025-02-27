using BookHaven.Core.Contracts;
using BookHaven.Core.Entities;
using BookHaven.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(string userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Book)
                .ToListAsync();
        }
    }
}
