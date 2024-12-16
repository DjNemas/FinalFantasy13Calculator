using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Database.Models
{
    public class Accessoire : Item
    {
        public uint? MinValue { get; set; } = null;
        public uint? MaxValue { get; set; } = null;
        [ForeignKey("UpgradeToAccessoire")]
        public uint? UpgradeToAccessoireId { get; set; } = null;
        public Accessoire? UpgradeToAccessoire { get; set; } = null;
    }
}
