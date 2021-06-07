using System;

namespace Webservice.API.ClientSide.Models
{
    public class FeedbackClient
    {
        public int Id { get; set; }
        public int Star { get; set; }
        public string Comment { get; set; }
        public int AccountId { get; set; }
        public int PlaceId { get; set; }
    }
}
