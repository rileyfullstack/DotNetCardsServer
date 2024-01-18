using DotNetCardsServer.Models.Users;
using MongoDB.Bson;

namespace DotNetCardsServer.Models.Cards
{
    public class Card
    {
    #pragma warning disable CS8618
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public Image Image { get; set; }
        public Address Address { get; set; }
        public string BizNumber { get; set; }
        public int Likes { get; set; }
        public string User_Id { get; set; }
    }
}
