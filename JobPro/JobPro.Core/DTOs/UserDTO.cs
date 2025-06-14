namespace JobPro.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        
        public bool IsBlocked { get; set; } = false;

        public DateTime CreatedAt { get; set; }
    }
}