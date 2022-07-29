using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
