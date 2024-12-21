namespace RestAPI.Models
{
    public class CurrentUser
    {
        public required uint Id { get; set; }
        public required Roles Role { get; set; }
    }
}
