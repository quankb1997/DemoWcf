using System.Runtime.Serialization;

namespace WcfClient.Models
{
    public class AccountModels
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
    }
}
