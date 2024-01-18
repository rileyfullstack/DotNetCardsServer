using DotNetCardsServer.Exceptions;
using DotNetCardsServer.Models.Users;
using DotNetCardsServer.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNetCardsServer.Services.Users
{
    public class UsersService
    {
        private IMongoCollection<User> _users;

        public UsersService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("dotnet_business_card_app");
            _users = database.GetCollection<User>("users");
        }

        public async Task<object> CreateUserAsync(User newUser)
        {
            var existingUser = await _users.Find(u => u.Email == newUser.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User with this email already exists.");
            }

            newUser.Password = PasswordHelper.GeneratePassword(newUser.Password);
            await _users.InsertOneAsync(newUser);
            return new { newUser.Id, newUser.UserName, newUser.Email};
        }

        //get users
        public async Task<List<User>> GetUsersAsync()
        {
            var builder = Builders<User>.Projection;
            var projection = builder.Exclude("Password");
            List<User> allUsers = await _users.Find(_ => true).Project<User>(projection).ToListAsync();
            return allUsers;
        }

        //get one user
        public async Task<User> GetUserAsync(string userId)
        {
            var builder = Builders<User>.Projection;
            var projection = builder.Exclude("Password");
            User specificUser = await _users.Find(u => u.Id.ToString() == userId).Project<User>(projection).FirstOrDefaultAsync();
            if (specificUser == null)
            {
                //exception user not found
                throw new UserDoesntExistException(userId);
            }
            return specificUser;
        }
        //delete user
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var result = await _users.DeleteOneAsync(u => u.Id.ToString() == userId);
            if (result.DeletedCount == 0)
            {
                throw new UserDoesntExistException("The user to delete doesn't exist.");
            }
            return true;
        }
        //edit user
        public async Task<User> EditUserAsync(string userId, User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, new ObjectId(userId));

            var update = Builders<User>.Update
                .Set(u => u.UserName, updatedUser.UserName)
                .Set(u => u.Email, updatedUser.Email)
                .Set(u => u.UserAddress, updatedUser.UserAddress)
                .Set(u => u.Phone, updatedUser.Phone)
                .Set(u => u.IsBusiness, updatedUser.IsBusiness)
                .Set(u => u.IsAdmin, updatedUser.IsAdmin)
                .Set(u => u.UserImage, updatedUser.UserImage);

            var result = await _users.UpdateOneAsync(filter, update);

            // Check if the update was successful
            if (result.MatchedCount == 0)
            {
                throw new UserDoesntExistException(userId);
            }

            updatedUser.Password = "";
            return updatedUser;
        }

        //login
        public async Task<User> LoginAsync(LoginModel loginModel)
        {
            var builder = Builders<User>.Projection;
            var projection = builder.Exclude("Password");

            var userLogin = await _users.Find(u => u.Email == loginModel.Email).Project<User>(projection).FirstOrDefaultAsync();
            if (userLogin == null)
            {
                throw new AuthenticationException("Login failed (User with recived email does not exist)");
            }
            if (PasswordHelper.VerifyPassword(userLogin.Password, loginModel.Password))
            {
                throw new AuthenticationException("Login failed (Entered password is wrong.)");
            }

            return userLogin;
        }

    }

}
