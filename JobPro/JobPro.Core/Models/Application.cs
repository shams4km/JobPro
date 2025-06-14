namespace JobPro.Core.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ResumePath { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}