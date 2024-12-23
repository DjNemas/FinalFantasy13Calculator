namespace Shared.DTOs
{
    public class ResetRequest
    {
        public required uint UserId { get; set; }
        public required string ResetToken { get; set; }
    }
}
