using MongoDB.Bson;

namespace DotNetCardsServer.Models.Users
{
    public class User
    {
        public ObjectId Id { get; set; }
        public Name UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Image UserImage { get; set; }
        public Address UserAddress { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBusiness { get; set; }
        public DateTime UserCreationDate { get; set; }

        public User(Name userName, string email, string password, string phone, Image userImage, Address userAddress, bool isAdmin = false, bool isBusiness = false, DateTime userCreationDate = default)
        {
            UserName = userName;
            Email = email;
            Password = password;
            Phone = phone;
            UserImage = userImage;
            UserAddress = userAddress;
            IsAdmin = isAdmin;
            IsBusiness = isBusiness;
            UserCreationDate = userCreationDate == default ? DateTime.Now : userCreationDate;
        }
    }

}
