using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Travel.WebApi.ClientSide.Models
{
    public class PlaceClient
    {
        public PlaceClient()
        {
        }
        public PlaceClient(int id, string title, string content, int userId, string fileBase64)
        {
            Id = id;
            Title = title;
            Content = content;
            UserId = userId;
            FileBase64 = fileBase64;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string FileBase64 { get; set; }
        public string Link { get; set; }
    }
}
