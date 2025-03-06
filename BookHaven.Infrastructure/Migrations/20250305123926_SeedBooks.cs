using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHaven.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "GenreId", "ImageName", "PublicationDate", "Title" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", "Bilbo Baggins, a peaceful hobbit, is suddenly swept into an epic adventure when the wizard Gandalf and a group of dwarves enlist him to help reclaim their homeland from the dragon Smaug. Along the way, Bilbo encounters trolls, goblins, and a strange creature named Gollum, from whom he acquires a mysterious ring of great power.", 1, null, new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hobbit" },
                    { 2, "J.K. Rowling", "An orphaned boy, Harry Potter, discovers on his eleventh birthday that he is a wizard and has been accepted into Hogwarts School of Witchcraft and Wizardry. As he learns magic, he unravels the mystery of his parents' fate and comes face to face with the dark wizard Voldemort, who seeks the powerful Sorcerer’s Stone to regain his lost strength.", 1, null, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Sorcerer's Stone" },
                    { 3, "Jane Austen", "In early 19th century England, Elizabeth Bennet navigates the expectations of society, love, and personal growth. When she meets the wealthy and reserved Mr. Darcy, they clash due to their pride and prejudice, but as circumstances unfold, they both learn valuable lessons about themselves and each other.", 2, null, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pride and Prejudice" },
                    { 4, "Nicholas Sparks", "Noah Calhoun and Allie Nelson fall deeply in love one summer, but societal expectations and fate tear them apart. Years later, their love story is retold through the pages of a notebook, proving that true love endures even when faced with obstacles like time, distance, and memory loss.", 2, null, new DateTime(1996, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Notebook" },
                    { 5, "Gillian Flynn", "On the morning of their fifth wedding anniversary, Amy Dunne goes missing, and her husband, Nick, becomes the prime suspect. As the police and media unravel the details of their complicated marriage, shocking secrets and mind games come to light, leading to an unpredictable and chilling conclusion.", 3, null, new DateTime(2012, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gone Girl" },
                    { 6, "Stieg Larsson", "Investigative journalist Mikael Blomkvist and the brilliant but troubled hacker Lisbeth Salander are drawn into the decades-old mystery of a wealthy industrialist’s missing niece. As they dig deeper into the family’s dark secrets, they uncover a horrifying truth that someone is willing to kill to protect.", 3, null, new DateTime(2005, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Girl with the Dragon Tattoo" },
                    { 7, "Frank Herbert", "Set on the desert planet Arrakis, young Paul Atreides is thrust into a complex political struggle over the control of the spice melange, the most valuable substance in the universe. As he discovers his own destiny and hidden abilities, he must navigate war, betrayal, and prophecy to determine the fate of his people.", 4, null, new DateTime(1965, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dune" },
                    { 8, "Isaac Asimov", "Mathematician Hari Seldon develops a science called psychohistory, which predicts the inevitable fall of the Galactic Empire. To minimize the dark age that will follow, he establishes the Foundation—a group of scientists and scholars tasked with preserving knowledge and guiding civilization toward a brighter future.", 4, null, new DateTime(1951, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Foundation" },
                    { 9, "Arthur Conan Doyle", "When the wealthy Sir Charles Baskerville dies under mysterious circumstances, his heir, Sir Henry, becomes the target of a supposed family curse involving a spectral hound. Famed detective Sherlock Holmes and his trusted companion, Dr. Watson, investigate the eerie events, uncovering a sinister plot in the moors of Devonshire.", 5, null, new DateTime(1902, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sherlock Holmes: The Hound of the Baskervilles" },
                    { 10, "Dan Brown", "When symbologist Robert Langdon is called to investigate a mysterious murder in the Louvre Museum, he uncovers a trail of cryptic clues hidden in famous works of art. As he races against time with cryptologist Sophie Neveu, they reveal a hidden secret that could change the course of history forever.", 5, null, new DateTime(2003, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Da Vinci Code" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
