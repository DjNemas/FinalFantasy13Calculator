namespace RestAPI.Database.Models
{
    public class UserRole
    {
        [Key]
        public uint Id { get; set; }
        public required Roles Role { get; set; }
    }
}
