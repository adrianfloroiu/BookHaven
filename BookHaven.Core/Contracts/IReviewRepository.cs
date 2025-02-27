﻿using BookHaven.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHaven.Core.Contracts
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(string userId);
    }
}
