using DotNetCardsServer.Models.Users;

namespace DotNetCardsServer.Models.MockData
{
    public class MockUsers
    {
        static public List<User> UserList = new List<User>
        {
            new User(
                id: 1,
                userName: new Name("John", "Doe", "Middle"),
                email: "john.doe@example.com",
                password: "password123",
                phone: "123-456-7890",
                userImage: new Image("https://example.com/image1.jpg", "John's Image"),
                userAddress: new Address("State1", "Country1", "City1", "Street1", 123, 45678),
                isAdmin: false,
                isBusiness: false),

            new User(
                id: 2,
                userName: new Name("Jane", "Doe", "Middle"),
                email: "jane.doe@example.com",
                password: "password456",
                phone: "234-567-8901",
                userImage: new Image("https://example.com/image2.jpg", "Jane's Image"),
                userAddress: new Address("State2", "Country2", "City2", "Street2", 456, 56789),
                isAdmin: false,
                isBusiness: true),

            new User(
                id: 3,
                userName: new Name("James", "Smith", "Middle"),
                email: "james.smith@example.com",
                password: "password789",
                phone: "345-678-9012",
                userImage: new Image("https://example.com/image3.jpg", "James' Image"),
                userAddress: new Address("State3", "Country3", "City3", "Street3", 789, 67890),
                isAdmin: true,
                isBusiness: true),
        };
    }
}
