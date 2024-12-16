using RestAPI.Database.Enums;
using System.Text.Json.Serialization;

namespace RestAPI.DTOs
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
        public required NexusGroup NexusGroup { get; set; }
        public Catalysator Catalysator { get; set; } = Catalysator.None;

        [System.ComponentModel.DefaultValue(null)]
        public string? SpecialEffect { get; set; } = null;

        [System.ComponentModel.DefaultValue(false)]
        public bool Buyable { get; set; } = false;

        [System.ComponentModel.DefaultValue(null)]
        public uint? BuyPrice { get; set; } = null;
        
    }
}
