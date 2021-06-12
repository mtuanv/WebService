using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebService.ClientSide.Models
{
    public class PlaceClient
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
