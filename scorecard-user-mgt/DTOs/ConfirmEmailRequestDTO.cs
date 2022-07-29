using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.DTOs
{
    public class ConfirmEmailRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
