using BookHaven.Core.Entities;

namespace BookHaven.Core.DTO
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string GenreName { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public double AverageRating { get; set; }
    }
}
