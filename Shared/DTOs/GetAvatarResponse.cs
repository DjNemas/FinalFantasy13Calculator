namespace Shared.DTOs
{
    public class GetAvatarResponse
    {
        public required string Base64AvatarImage { get; set; }
        public required string AvatarImageMimeType { get; set; }
    }
}
