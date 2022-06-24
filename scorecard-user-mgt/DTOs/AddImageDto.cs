using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace scorecard_user_mgt.DTOs
{
    public class AddImageDto
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
