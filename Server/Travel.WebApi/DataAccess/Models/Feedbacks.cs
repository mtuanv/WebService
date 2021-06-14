using Travel.WebApi.DataAccess.Extensions;

namespace Travel.WebApi.DataAccess.Models
{
    public partial class Feedbacks : BaseEntity
    {
        public int? RateStar { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public virtual Places Place { get; set; }
        public virtual Users User { get; set; }
    }
}
