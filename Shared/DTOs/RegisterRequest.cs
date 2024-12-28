using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class RegisterRequest
    {
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 12)]
        public required string Password { get; set; }
    }
}
