using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class UpdateAccessoireRequest : ItemRequest
    {
        [JsonPropertyOrder(1)]
        public required uint Id { get; set; }

        [System.ComponentModel.DefaultValue(null)]
        public uint? MinValue { get; set; } = null;

        [System.ComponentModel.DefaultValue(null)]
        public uint? MaxValue { get; set; } = null;

        [System.ComponentModel.DefaultValue(null)]
        public uint? UpgradeToAccessoireId { get; set; } = null;
    }
}
