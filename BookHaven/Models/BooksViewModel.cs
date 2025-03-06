using BookHaven.Core.DTO;
using BookHaven.Core.Entities;
using BookHaven.Core.Helpers;

namespace BookHaven.Models
{
    public class BooksViewModel
    {
        public List<BookListDto> Books { get; set; }
        public PaginatedList<Book> PaginatedList { get; set; }
    }
}
