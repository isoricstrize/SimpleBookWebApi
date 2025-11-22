using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleBookWebApi.Data;
using SimpleBookWebApi.Dtos;
using SimpleBookWebApi.Entities;

namespace SimpleBookWebApi.Services
{
    // -----------------------------------------------------------------------------
    // AuthService
    // 
    // This service handles all authentication-related operations, including:
    //
    // • User registration – hashing the password and saving the user securely.
    // • User login – verifying user and generating JWT + refresh tokens.
    // • Creating JWT tokens – embedding user claims such as ID, username, and role.
    // • Refresh token – generating, storing, validating refresh tokens.
    // 
    // AuthService contains all logic used by AuthController so the controller
    // remains clean and focused only on receiving requests and sending responses.
    // -----------------------------------------------------------------------------

    public class AuthService(BookDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<ApplicationUser?> RegisterAsync(ApplicationUserDto request)
        {
            if (await context.ApplicationUsers.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }

            var user = new ApplicationUser();

            var hashedPassword = new PasswordHasher<ApplicationUser>()
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;

            context.ApplicationUsers.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<TokenResponseDto?> LoginAsync(ApplicationUserDto request)
        {
            var user = await context.ApplicationUsers.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<ApplicationUser>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseDto> CreateTokenResponse(ApplicationUser? user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;

            return await CreateTokenResponse(user);
        }

        // Check if user refresh token is validate
        private async Task<ApplicationUser?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.ApplicationUsers.FindAsync(userId);

            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(ApplicationUser user)
        {
            // claims -> contains everything that we put into JWT (username, psw, role...)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // token descriptor that describes our JWT
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"), // who is issuing this token
                audience: configuration.GetValue<string>("AppSettings:Audience"), // who are using this token
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            // serializes JWTSecurityToken into a JWT in compact serialization format 
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}