using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 250 characters!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 250 characters!")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Phone number must be between 2 and 50 characters!")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 250 characters!")]
        public string Address { get; set; }
    }
}