namespace Travel.WebApi.ClientSide.Models
{
    public class FeedbackClient
    {
        public int Id { get; set; }
        public int? RateStar { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
    }
}
