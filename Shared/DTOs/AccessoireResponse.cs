namespace Shared.DTOs
{
    public class AccessoireResponse : ItemResponse
    {
        public uint? MinValue { get; set; } = null;
        public uint? MaxValue { get; set; } = null;
        public uint? UpgradeToAccessoireId { get; set; } = null;
    }
}
