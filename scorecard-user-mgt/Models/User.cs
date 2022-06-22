using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.Models
{
    public class User 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 50 characters!")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 50 characters!")]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Invalid Email!")]
        public string Email { get; set; }
    }
}
