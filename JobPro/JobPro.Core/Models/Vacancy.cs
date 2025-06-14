namespace JobPro.Core.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Salary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}