using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebService.DataAccess.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int? Star { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int? PlaceId { get; set; }

        public virtual Place Place { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
