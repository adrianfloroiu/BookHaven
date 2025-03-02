using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHaven.Core.DTO
{
    public class BookCreateDto
    {
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

        [Required(ErrorMessage = "Please select a genre")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
    }
}
