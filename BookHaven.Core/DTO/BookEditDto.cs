using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookHaven.Core.DTO
{
    public class BookEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Publication date is required")]
        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Cover Image")]
        public IFormFile? Image { get; set; }

        [Display(Name = "Current Image")]
        public string? CurrentImageName { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
    }
}
