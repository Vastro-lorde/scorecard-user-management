using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.Models
{
    public class User : IdentityUser
    {
        public string PublicId { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 250 characters!")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 250 characters!")]
        public string LastName { get; set; }

        [StringLength(250, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 250 characters!")]
        public string Address { get; set; }

        [StringLength(50, MinimumLength = 0, ErrorMessage = "Gender must be between 0 and 50 characters!")]
        public string Gender { get; set; }

        [StringLength(250, MinimumLength = 3, ErrorMessage = "Avatar must be between 3 and 250 characters!")]
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
    }
}