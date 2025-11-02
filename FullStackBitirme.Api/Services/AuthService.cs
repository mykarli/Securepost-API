using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FullStackBitirme.Api.Data;
using FullStackBitirme.Api.Models;

namespace FullStackBitirme.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(string fullName, string email, string password)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
                throw new Exception("Bu email adresi zaten kayıtlı.");

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = password,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
