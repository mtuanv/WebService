using System.Collections.Generic;
using System.Text.Json.Serialization;
using Travel.WebApi.DataAccess.Extensions;

namespace Travel.WebApi.DataAccess.Models
{
    public partial class Users : BaseEntity
    {
        public Users()
        {
            Feedbacks = new HashSet<Feedbacks>();
            Places = new HashSet<Places>();
        }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountType { get; set; }

        public virtual ICollection<Feedbacks> Feedbacks { get; set; }
        public virtual ICollection<Places> Places { get; set; }
    }
}
