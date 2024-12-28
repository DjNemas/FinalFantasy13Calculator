namespace RestAPI.Database.Models
{
    public class User
    {
        [Key]
        public uint Id { get; set; }
        public required string Email { get; set; }
        [MinLength(4)]
        public required string Username { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required UserRole Role { get; set; }
        public string? Base64AvatarImage { get; set; }
        public string? AvatarImageMimeType { get; set; }
    }
}
