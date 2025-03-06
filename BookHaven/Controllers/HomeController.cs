using BookHaven.Core.Contracts;
using BookHaven.Core.DTO;
using BookHaven.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookHaven.Controllers;

public class HomeController : Controller
{
    private readonly IBookRepository _bookRepository;

    public HomeController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IActionResult> Index()
    {
        var topRatedBooks = await _bookRepository.GetHighestRatedBooks(4);
        var bookDTOs = topRatedBooks.Select(b => new BookListDto
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

        return View(bookDTOs);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
