using System.ComponentModel.DataAnnotations;

namespace Travel.WebApi.ClientSide.Authentication.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
