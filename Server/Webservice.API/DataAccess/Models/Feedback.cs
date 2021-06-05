using System;
using Webservice.API.DataAccess.Extensions;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webservice.API.DataAccess.Models
{
    public partial class Feedback : AuditedEntity<int>, IDeleteable
    {
        public int Star { get; set; }
        public string Comment { get; set; }
        public int AccountId { get; set; }
        public int PlaceId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Place Place { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
