using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class UserResponse
    {   
        public uint Id { get; set; }
        public required string Username { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Roles Role { get; set; }
        public string? Base64AvatarImage { get; set; }
        public string? AvatarImageMimeType { get; set; }
    }
}
