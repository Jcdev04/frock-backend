using Frock_backend.access_and_identity.Application.DTOs;
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

        public async Task<UserResponseDto> RegisterAsync(RegisterUserDto registerDto)
        {
            var email = new Email(registerDto.Email);

            // Verificar si el email ya existe
            if (await _userRepository.ExistsByEmailAsync(email))
            {
                throw new InvalidOperationException("A user with this email already exists");
            }

            // Crear nuevo usuario
            var user = new User(
                registerDto.FirstName,
                registerDto.LastName,
                registerDto.Email,
                registerDto.Password,
                registerDto.Role
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDto?> LoginAsync(LoginUserDto loginDto)
        {
            var email = new Email(loginDto.Email);
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !user.VerifyPassword(loginDto.Password))
            {
                return null;
            }

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
