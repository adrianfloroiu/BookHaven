namespace BookHaven.Core.DTO
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string GenreName { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImagePath { get; set; }
    }
}
