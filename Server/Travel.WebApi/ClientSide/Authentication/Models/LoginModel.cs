using System.ComponentModel.DataAnnotations;

namespace Travel.WebApi.ClientSide.Authentication.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
