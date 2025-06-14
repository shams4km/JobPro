
using JobPro.Core.DTOs;

public interface IJwtGenerator
{
    string GenerateToken(UserDTO user);
}