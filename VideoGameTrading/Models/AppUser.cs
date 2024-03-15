using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameTrading.Models
{
    public class AppUser : IdentityUser
    {
        public int AppUserId { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string? Name { get; set; }

        [NotMapped]
        public IList<string>? RoleNames { get; set; }
    }
}
