using System.Collections.Generic;
using Travel.WebApi.DataAccess.Extensions;

namespace Travel.WebApi.DataAccess.Models
{
    public partial class Places : BaseEntity
    {
        public Places()
        {
            Feedbacks = new HashSet<Feedbacks>();
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string Link { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Feedbacks> Feedbacks { get; set; }
    }
}
