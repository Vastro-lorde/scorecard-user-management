using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.Models
{
    public class User 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
