namespace RestAPI.Database.Models
{
    public class Session
    {
        [Key]
        public uint Id { get; set; }
        public required User User { get; set; }
        public required string BearerToken { get; set; }
        public DateTimeOffset ExpirationDateBearerToken { get; set; } = DateTimeOffset.UtcNow.AddHours(1);

        public required string ResetToken { get; set; }
        public DateTimeOffset ExpirationDateResetToken { get; set; } = DateTimeOffset.UtcNow.AddDays(1);
    }
}
