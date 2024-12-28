using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class LoginRequest
    {
        [EmailAddress]
        public required string Email { get; set; }
        [PasswordPropertyText]
        public required string Password { get; set; }
    }
}
