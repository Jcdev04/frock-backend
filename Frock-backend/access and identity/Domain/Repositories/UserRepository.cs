using Frock_backend.access_and_identity.Domain.Entities;
using Frock_backend.access_and_identity.Domain.ValueObjects;

namespace Frock_backend.access_and_identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(Email email);
        Task<User?> GetByIdAsync(userID userId);
        Task<bool> ExistsByEmailAsync(Email email);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
