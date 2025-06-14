using System.ComponentModel.DataAnnotations;

namespace JobPro.Core.DTOs
{
    public class ApplicationDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public string ResumePath { get; set; }

        public string Comment { get; set; }

        [Required]
        public int VacancyId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}