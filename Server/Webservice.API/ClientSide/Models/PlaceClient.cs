namespace Webservice.API.ClientSide.Models
{
    public class PlaceClient
    {
        public PlaceClient()
        {
        }
        public PlaceClient(string name, string description, string link, int accountId)
        {
            Name = name;
            Description = description;
            Link = link;
            AccountId = accountId;
        }
        public PlaceClient(int id, string name, string description, string link, int accountId)
        {
            Id = id;
            Name = name;
            Description = description;
            Link = link;
            AccountId = accountId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int AccountId { get; set; }

    }
}
