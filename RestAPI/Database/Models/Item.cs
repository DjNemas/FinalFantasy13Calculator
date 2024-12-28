namespace RestAPI.Database.Models
{
    public abstract class Item
    {
        [Key]
        public uint Id { get; set; }
        public required string Name { get; set; }
        public required uint Rank { get; set; }
        public required uint MaxLevel { get; set; }
        public required uint BaseEXP { get; set; }
        public required uint IncreaseEXP { get; set; }
        public required uint SellPrice { get; set; }
        public required NexusGroup NexusGroup { get; set; }
        public Catalysator Catalysator { get; set; } = Catalysator.None;
        public string? SpecialEffect { get; set; } = null;
        public bool Buyable { get; set; } = false;
        public uint? BuyPrice { get; set; } = null;

    }
}
