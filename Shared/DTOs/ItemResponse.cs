using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public abstract class ItemResponse
    {
        [JsonPropertyOrder(1)]
        public uint Id { get; set; }
        [JsonPropertyOrder(2)]
        public required string Name { get; set; }
        public required uint Rank { get; set; }
        public required uint MaxLevel { get; set; }
        public required uint BaseEXP { get; set; }
        public required uint IncreaseEXP { get; set; }
        public required uint SellPrice { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required NexusGroup NexusGroup { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Catalysator Catalysator { get; set; } = Catalysator.None;
        public string? SpecialEffect { get; set; } = null;
        public bool Buyable { get; set; } = false;
        public uint? BuyPrice { get; set; } = null;

    }
}
