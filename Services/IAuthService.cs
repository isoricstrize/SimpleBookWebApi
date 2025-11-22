using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBookWebApi.Dtos;
using SimpleBookWebApi.Entities;

namespace SimpleBookWebApi.Services
{
    public interface IAuthService
    {
        Task<ApplicationUser?> RegisterAsync(ApplicationUserDto request);
        Task<TokenResponseDto?> LoginAsync(ApplicationUserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}