using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHaven.Core.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [StringLength(100)]
        public string ImageName { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
