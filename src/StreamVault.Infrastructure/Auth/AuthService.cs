using Microsoft.AspNetCore.Identity;
using StreamVault.Application.DTOs.Auth;
using StreamVault.Application.Interfaces.Services;
using StreamVault.Domain.Entities;

namespace StreamVault.Infrastructure.Auth;

public class AuthService(
    UserManager<User> userManager,
    JwtTokenGenerator tokenGenerator
) : IAuthService
{
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.Username,
            Email = dto.Email,
        };

        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Registration failed: {errors}");
        }

        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null) throw new Exception("Invalid credentials");

        var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid) throw new Exception("Invalid credentials");

        return await GenerateAuthResponse(user);
    }

    private async Task<AuthResponseDto> GenerateAuthResponse(User user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var token = tokenGenerator.GenerateToken(user, roles);

        return new AuthResponseDto(user.Id, user.Email!, user.UserName!, token);
    }
}