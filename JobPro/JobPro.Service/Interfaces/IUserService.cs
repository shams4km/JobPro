using JobPro.Core.DTOs;

public interface IUserService
{
    Task<UserDTO> Register(RegisterDTO dto);
    Task<UserDTO> Login(LoginDTO dto);
    Task<IEnumerable<UserDTO>> GetAll();
    Task Block(int id);
    Task ChangeRole(int id, string role);
}