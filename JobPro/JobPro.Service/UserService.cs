using System.Threading.Tasks;
using AutoMapper;
using JobPro.Core.DTOs;
using JobPro.Core.Models;
using JobPro.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace JobPro.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO> Register(RegisterDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> Login(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task Block(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsBlocked = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeRole(int id, string role)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Role = role;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}