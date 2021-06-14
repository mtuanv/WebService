using Travel.WebApi.ClientSide.Common;

namespace Travel.WebApi.ClientSide.Models
{
    public class UserClient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountType Type { get; set; }
    }
}
