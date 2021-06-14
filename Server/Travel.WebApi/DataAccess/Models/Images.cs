using Travel.WebApi.DataAccess.Extensions;

namespace Travel.WebApi.DataAccess.Models
{
    public partial class Images : BaseEntity
    {
        public string Path { get; set; }
        public int PlaceId { get; set; }

        public virtual Places Place { get; set; }
    }
}
