using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class RegisterRequest
    {
        
        [StringLength(30, MinimumLength = 4)]
        public required string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 12)]
        public required string Password { get; set; }
    }
}
