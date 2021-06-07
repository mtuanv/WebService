using Webservice.API.ClientSide.Common;

namespace Webservice.API.ClientSide.Models
{
    public class AccountClient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AccountRole Role { get; set; }
        public string Email { get; set; }
    }
}
