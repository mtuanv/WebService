namespace WebService.ClientSide.Models
{
    public class PlaceClient
    {
        public PlaceClient()
        {
        }
        public PlaceClient(string title, string content, string userId)
        {
            Title = title;
            Content = content;
            UserId = userId;
        }
        public PlaceClient(int id, string title, string content, string userId)
        {
            Id = id;
            Title = title;
            Content = content;
            UserId = userId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }

    }
}
