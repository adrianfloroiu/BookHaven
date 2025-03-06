using BookHaven.Core.Contracts;
using BookHaven.Core.DTO;
using BookHaven.Core.Entities;
using BookHaven.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookHaven.Controllers
{
    [Authorize(Roles = "admin")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const int booksPerPage = 5;

        public BooksController(
            IBookRepository bookRepository,
            IGenreRepository genreRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var paginatedBooks = await _bookRepository.GetPaginatedBooksAsync(pageIndex, booksPerPage);
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
                PaginatedList = paginatedBooks
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            await LoadGenres();
            return View(new BookCreateDto
            {
                PublicationDate = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateDto bookDTO)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = bookDTO.Title,
                    Author = bookDTO.Author,
                    Description = bookDTO.Description,
                    PublicationDate = bookDTO.PublicationDate,
                    GenreId = bookDTO.GenreId
                };

                // Upload image
                if (bookDTO.Image != null)
                {
                    book.ImageName = await SaveImage(bookDTO.Image);
                }

                await _bookRepository.AddAsync(book);
                await _bookRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadGenres();
            return View(bookDTO);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = new BookEditDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                PublicationDate = book.PublicationDate,
                GenreId = book.GenreId,
                CurrentImageName = book.ImageName
            };

            await LoadGenres();
            return View(bookDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookEditDto bookDTO)
        {
            if (id != bookDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var book = await _bookRepository.GetByIdAsync(id);
                    if (book == null)
                    {
                        return NotFound();
                    }

                    book.Title = bookDTO.Title;
                    book.Author = bookDTO.Author;
                    book.Description = bookDTO.Description;
                    book.PublicationDate = bookDTO.PublicationDate;
                    book.GenreId = bookDTO.GenreId;

                    // Delete old image if it exists and upload the new one
                    if (bookDTO.Image != null)
                    {
                        if (!string.IsNullOrEmpty(book.ImageName))
                        {
                            DeleteImage(book.ImageName);
                        }

                        book.ImageName = await SaveImage(bookDTO.Image);
                    }

                    await _bookRepository.UpdateAsync(book);
                    await _bookRepository.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred while updating the book.");
                }
            }

            await LoadGenres();
            return View(bookDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = new BookDeleteDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                GenreName = book.Genre.Name,
                ImagePath = !string.IsNullOrEmpty(book.ImageName)
                    ? $"/images/books/{book.ImageName}"
                    : "/images/book-placeholder.png"
            };

            return View(bookDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Delete image file if exists
            if (!string.IsNullOrEmpty(book.ImageName))
            {
                DeleteImage(book.ImageName);
            }

            await _bookRepository.DeleteAsync(book);
            await _bookRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region Helpers

        private async Task LoadGenres()
        {
            var genres = await _genreRepository.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            // Create unique filename
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);

            // Ensure directory exists
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        private void DeleteImage(string imageName)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books", imageName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        #endregion
    }
}