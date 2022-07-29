using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.DTOs
{
    public class UpdatePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "password must be between 2 and 50 characters in length")]
        public string NewPassword { get; set; }
    }
}
