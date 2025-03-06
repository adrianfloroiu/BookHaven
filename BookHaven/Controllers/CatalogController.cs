using BookHaven.Core.Contracts;
using BookHaven.Core.DTO;
using BookHaven.Core.Entities;
using BookHaven.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookHaven.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int booksPerPage = 8;

        public CatalogController(
            IBookRepository bookRepository,
            IGenreRepository genreRepository,
            IReviewRepository reviewRepository,
            UserManager<ApplicationUser> userManager)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? genreId, string? searchTerm, int pageIndex = 1)
        {
            ViewBag.Genres = await _genreRepository.GetAllAsync();
            ViewBag.SelectedGenreId = genreId;
            ViewBag.SearchTerm = searchTerm;

            var paginatedBooks = await _bookRepository.GetPaginatedBooksAsync(pageIndex, booksPerPage, genreId, searchTerm);

            var bookDTOs = paginatedBooks.Select(b => new BookListDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                GenreName = b.Genre.Name,
                PublicationDate = b.PublicationDate,
                ImagePath = !string.IsNullOrEmpty(b.ImageName)
                    ? $"/images/books/{b.ImageName}"
                    : "/images/book-placeholder.png"
            });

            var viewModel = new BooksViewModel
            {
                Books = bookDTOs.ToList(),
                PaginatedList = paginatedBooks,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDetailDto = new BookDetailsDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                GenreName = book.Genre.Name,
                PublicationDate = book.PublicationDate,
                ImagePath = !string.IsNullOrEmpty(book.ImageName)
                    ? $"/images/books/{book.ImageName}"
                    : "/images/book-placeholder.png",
                Reviews = book.Reviews.OrderByDescending(r => r.Id).ToList(),
                AverageRating = book.Reviews.Any() ? book.Reviews.Average(r => r.Rating) : 0
            };

            // Review form
            ViewBag.ReviewForm = new ReviewCreateDto { BookId = id };

            return View(bookDetailDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddReview(ReviewCreateDto reviewDto)
        {
            if (ModelState.IsValid)
            {
                var review = new Review
                {
                    BookId = reviewDto.BookId,
                    Rating = reviewDto.Rating,
                    Comment = reviewDto.Comment,
                    UserId = _userManager.GetUserId(User)!
                };

                await _reviewRepository.AddAsync(review);
                await _reviewRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = reviewDto.BookId });
            }

            // Something failed, redisplay form
            var book = await _bookRepository.GetBookWithDetailsAsync(reviewDto.BookId);
            var bookDetailDto = new BookDetailsDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                GenreName = book.Genre.Name,
                PublicationDate = book.PublicationDate,
                ImagePath = !string.IsNullOrEmpty(book.ImageName)
                    ? $"/images/books/{book.ImageName}"
                    : "/images/book-placeholder.png",
                Reviews = book.Reviews.OrderByDescending(r => r.Id).ToList(),
                AverageRating = book.Reviews.Any() ? book.Reviews.Average(r => r.Rating) : 0
            };

            ViewBag.ReviewForm = reviewDto;

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                ModelState.AddModelError("", error.ErrorMessage);
            }

            return View("Details", bookDetailDto);
        }
    }
}