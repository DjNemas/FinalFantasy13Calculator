using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestAPI.Database.Models
{
    public class Weapon : Item
    {
        public required uint MinPhysicalDamage { get; set; }
        public required uint MaxPhysicalDamage { get; set; }
        public required uint MinMagicDamage { get; set; }
        public required uint MaxMagicDamage { get; set; }
        [ForeignKey("UpgradeToWeapon")]
        public uint? UpgradeToWeaponId { get; set; } = null;
        public Weapon? UpgradeToWeapon { get; set; } = null;
    }
}
