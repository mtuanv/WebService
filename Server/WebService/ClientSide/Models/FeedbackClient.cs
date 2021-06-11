using System;

namespace WebService.ClientSide.Models
{
    public class FeedbackClient
    {
        public int Id { get; set; }
        public int Star { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int PlaceId { get; set; }
    }
}
