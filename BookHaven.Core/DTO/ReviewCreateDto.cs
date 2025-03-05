using System.ComponentModel.DataAnnotations;

namespace BookHaven.Core.DTO
{
    public class ReviewCreateDto
    {
        [Required]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please select a rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Please enter your review")]
        [StringLength(1000, ErrorMessage = "Your review cannot exceed 1000 characters")]
        public string Comment { get; set; }
    }
}
