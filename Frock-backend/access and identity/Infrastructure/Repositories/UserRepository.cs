using Frock_backend.access_and_identity.Domain.Entities;
using Frock_backend.access_and_identity.Domain.Repositories;
using Frock_backend.access_and_identity.Domain.ValueObjects;
using Frock_backend.access_and_identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Frock_backend.access_and_identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AccessIdentityDbContext _context;

        public UserRepository(AccessIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(userID userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> ExistsByEmailAsync(Email email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
