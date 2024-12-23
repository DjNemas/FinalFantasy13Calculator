using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class GetUserResponse
    {   
        public uint Id { get; set; }
        public required string Username { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Roles Role { get; set; }
    }
}
