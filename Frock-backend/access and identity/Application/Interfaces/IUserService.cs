using Frock_backend.access_and_identity.Domain.Entities;
using Frock_backend.access_and_identity.Domain.ValueObjects;

namespace Frock_backend.access_and_identity.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string firstName, string lastName, string email, string password, UserRole role);
        Task<User?> LoginAsync(string email, string password);
    }
}
