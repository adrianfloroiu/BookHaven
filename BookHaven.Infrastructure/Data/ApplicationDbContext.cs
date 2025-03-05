using BookHaven.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Book - Genre
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book - Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // User - Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fantasy" },
                new Genre { Id = 2, Name = "Romance" },
                new Genre { Id = 3, Name = "Thriller" },
                new Genre { Id = 4, Name = "Science Fiction" },
                new Genre { Id = 5, Name = "Mystery" }
            );

            // Seed initial books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Description = "Bilbo Baggins, a peaceful hobbit, is suddenly swept into an epic adventure when the wizard Gandalf and a group of dwarves enlist him to help reclaim their homeland from the dragon Smaug. Along the way, Bilbo encounters trolls, goblins, and a strange creature named Gollum, from whom he acquires a mysterious ring of great power.",
                    PublicationDate = new DateTime(1937, 9, 21),
                    GenreId = 1,
                    ImageName = null
                },
                new Book
                {
                    Id = 2,
                    Title = "Harry Potter and the Sorcerer's Stone",
                    Author = "J.K. Rowling",
                    Description = "An orphaned boy, Harry Potter, discovers on his eleventh birthday that he is a wizard and has been accepted into Hogwarts School of Witchcraft and Wizardry. As he learns magic, he unravels the mystery of his parents' fate and comes face to face with the dark wizard Voldemort, who seeks the powerful Sorcerer’s Stone to regain his lost strength.",
                    PublicationDate = new DateTime(1997, 6, 26),
                    GenreId = 1,
                    ImageName = null
                },
                new Book
                {
                    Id = 3,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Description = "In early 19th century England, Elizabeth Bennet navigates the expectations of society, love, and personal growth. When she meets the wealthy and reserved Mr. Darcy, they clash due to their pride and prejudice, but as circumstances unfold, they both learn valuable lessons about themselves and each other.",
                    PublicationDate = new DateTime(1813, 1, 28),
                    GenreId = 2,
                    ImageName = null
                },
                new Book
                {
                    Id = 4,
                    Title = "The Notebook",
                    Author = "Nicholas Sparks",
                    Description = "Noah Calhoun and Allie Nelson fall deeply in love one summer, but societal expectations and fate tear them apart. Years later, their love story is retold through the pages of a notebook, proving that true love endures even when faced with obstacles like time, distance, and memory loss.",
                    PublicationDate = new DateTime(1996, 10, 1),
                    GenreId = 2,
                    ImageName = null
                },
                new Book
                {
                    Id = 5,
                    Title = "Gone Girl",
                    Author = "Gillian Flynn",
                    Description = "On the morning of their fifth wedding anniversary, Amy Dunne goes missing, and her husband, Nick, becomes the prime suspect. As the police and media unravel the details of their complicated marriage, shocking secrets and mind games come to light, leading to an unpredictable and chilling conclusion.",
                    PublicationDate = new DateTime(2012, 5, 24),
                    GenreId = 3,
                    ImageName = null
                },
                new Book
                {
                    Id = 6,
                    Title = "The Girl with the Dragon Tattoo",
                    Author = "Stieg Larsson",
                    Description = "Investigative journalist Mikael Blomkvist and the brilliant but troubled hacker Lisbeth Salander are drawn into the decades-old mystery of a wealthy industrialist’s missing niece. As they dig deeper into the family’s dark secrets, they uncover a horrifying truth that someone is willing to kill to protect.",
                    PublicationDate = new DateTime(2005, 8, 1),
                    GenreId = 3,
                    ImageName = null
                },
                new Book
                {
                    Id = 7,
                    Title = "Dune",
                    Author = "Frank Herbert",
                    Description = "Set on the desert planet Arrakis, young Paul Atreides is thrust into a complex political struggle over the control of the spice melange, the most valuable substance in the universe. As he discovers his own destiny and hidden abilities, he must navigate war, betrayal, and prophecy to determine the fate of his people.",
                    PublicationDate = new DateTime(1965, 6, 1),
                    GenreId = 4,
                    ImageName = null
                },
                new Book
                {
                    Id = 8,
                    Title = "Foundation",
                    Author = "Isaac Asimov",
                    Description = "Mathematician Hari Seldon develops a science called psychohistory, which predicts the inevitable fall of the Galactic Empire. To minimize the dark age that will follow, he establishes the Foundation—a group of scientists and scholars tasked with preserving knowledge and guiding civilization toward a brighter future.",
                    PublicationDate = new DateTime(1951, 5, 1),
                    GenreId = 4,
                    ImageName = null
                },
                new Book
                {
                    Id = 9,
                    Title = "Sherlock Holmes: The Hound of the Baskervilles",
                    Author = "Arthur Conan Doyle",
                    Description = "When the wealthy Sir Charles Baskerville dies under mysterious circumstances, his heir, Sir Henry, becomes the target of a supposed family curse involving a spectral hound. Famed detective Sherlock Holmes and his trusted companion, Dr. Watson, investigate the eerie events, uncovering a sinister plot in the moors of Devonshire.",
                    PublicationDate = new DateTime(1902, 4, 1),
                    GenreId = 5,
                    ImageName = null
                },
                new Book
                {
                    Id = 10,
                    Title = "The Da Vinci Code",
                    Author = "Dan Brown",
                    Description = "When symbologist Robert Langdon is called to investigate a mysterious murder in the Louvre Museum, he uncovers a trail of cryptic clues hidden in famous works of art. As he races against time with cryptologist Sophie Neveu, they reveal a hidden secret that could change the course of history forever.",
                    PublicationDate = new DateTime(2003, 3, 18),
                    GenreId = 5,
                    ImageName = null
                }
            );
        }
    }
}
