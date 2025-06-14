using Frock_backend.access_and_identity.Application.DTOs;

namespace Frock_backend.access_and_identity.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(RegisterUserDto registerDto);
        Task<UserResponseDto?> LoginAsync(LoginUserDto loginDto);
    }
}
