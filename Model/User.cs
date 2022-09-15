using System.Text.Json.Serialization;

namespace ViewAdAPI.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public int Coins { get; set; }
        public string? DeviceToken { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}