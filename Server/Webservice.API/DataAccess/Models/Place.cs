using System;
using System.Collections.Generic;
using Webservice.API.DataAccess.Extensions;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webservice.API.DataAccess.Models
{
    public partial class Place : AuditedEntity<int>, IDeleteable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
