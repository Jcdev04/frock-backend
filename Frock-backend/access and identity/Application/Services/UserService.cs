using Frock_backend.access_and_identity.Application.Interfaces;
using Frock_backend.access_and_identity.Domain.Entities;
using Frock_backend.access_and_identity.Domain.Repositories;
using Frock_backend.access_and_identity.Domain.ValueObjects;

namespace Frock_backend.access_and_identity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(string firstName, string lastName, string email, string password, UserRole role)
        {
            var emailVO = new Email(email);

            // Verificar si el email ya existe
            if (await _userRepository.ExistsByEmailAsync(emailVO))
            {
                throw new InvalidOperationException("A user with this email already exists");
            }

            // Crear nuevo usuario
            var user = new User(firstName, lastName, email, password, role);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var emailVO = new Email(email);
            var user = await _userRepository.GetByEmailAsync(emailVO);

            if (user == null || !user.VerifyPassword(password))
            {
                return null;
            }

            return user;
        }
    }
}
