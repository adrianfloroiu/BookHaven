using Microsoft.AspNetCore.Identity;

namespace BookHaven.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
