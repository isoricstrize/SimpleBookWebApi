using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookWebApi.Dtos
{
    /// <summary>
    /// DTO used for user registration and login.
    /// We only expose non-sensitive fields to the client.
    /// This prevents leaking internal or sensitive data from the ApplicationUser entity.
    /// <summary>
    public class ApplicationUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}