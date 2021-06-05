using System;
using System.Collections.Generic;
using Webservice.API.DataAccess.Extensions;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webservice.API.DataAccess.Models
{
    public partial class Account : AuditedEntity<int>, IDeleteable
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Place> Place { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
