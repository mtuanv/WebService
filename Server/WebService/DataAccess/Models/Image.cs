using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.DataAccess.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int? PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
