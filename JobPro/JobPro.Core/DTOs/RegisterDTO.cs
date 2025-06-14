using System.ComponentModel.DataAnnotations;

namespace JobPro.Core.DTOs
{
    public class RegisterDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        public string Role { get; set; } = "user";
    }
}