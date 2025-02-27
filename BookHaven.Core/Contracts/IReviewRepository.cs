using BookHaven.Core.Entities;

namespace BookHaven.Core.Contracts
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(string userId);
    }
}
