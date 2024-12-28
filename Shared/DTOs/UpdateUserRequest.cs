using System.ComponentModel;

namespace Shared.DTOs
{
    public class UpdateUserRequest
    {
        public string? Username { get; set; }
        [Description("Base64 encoded image")]
        public string? Base64AvatarImage { get; set; }
    }
}
