using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.DTOs
{
    public class UserRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
