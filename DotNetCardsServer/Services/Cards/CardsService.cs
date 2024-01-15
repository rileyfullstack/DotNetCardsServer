using DotNetCardsServer.Exceptions;
using DotNetCardsServer.Models.Cards;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DotNetCardsServer.Services.Cards
{
    public class CardsService
    {
        private IMongoCollection<Card> _cards;

        public CardsService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("dotnet_business_card_app");
            _cards = database.GetCollection<Card>("cards");
        }

        public async Task<Card> CreateCardAsync(Card newCard)
        {
            var existingUser = await _cards.Find(c => (c.Title == newCard.Title) && (c.User_Id == newCard.User_Id)).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new CardAlreadyExistsException("Card with this title already exists for this user.");
            }

            await _cards.InsertOneAsync(newCard);
            return newCard;
        }

        //get users
        public async Task<List<Card>> GetCardsAsync()
        {
            List<Card> allCards = await _cards.Find(_ => true).ToListAsync();
            return allCards;
        }

        //get one user
        public async Task<Card> GetCardAsync(string cardId)
        {
            Card specificCard = await _cards.Find(u => u._id.ToString() == cardId).FirstOrDefaultAsync();
            if (specificCard == null)
            {
                //exception user not found
                throw new CardDoesntExistException("The requested card doesn't exist.");
            }
            return specificCard;
        }
        //delete user
        public async Task<bool> DeleteCardAsync(string cardId)
        {
            var result = await _cards.DeleteOneAsync(u => u._id.ToString() == cardId);
            if (result.DeletedCount == 0)
            {
                throw new CardDoesntExistException("The card to delete doesn't exist.");
            }
            return true;
        }

        //edit user
        public async Task<Card> EditCardAsync(string cardId, Card updatedCard)
        {
            var filter = Builders<Card>.Filter.Eq(u => u._id, new ObjectId(cardId));

            var update = Builders<Card>.Update
                .Set(c => c.Title, updatedCard.Title)
                .Set(c => c.Subtitle, updatedCard.Subtitle)
                .Set(c => c.Description, updatedCard.Description)
                .Set(c => c.Phone, updatedCard.Phone)
                .Set(c => c.Email, updatedCard.Email)
                .Set(c => c.Web, updatedCard.Web)
                .Set(c => c.Image, updatedCard.Image)
                .Set(c => c.Address, updatedCard.Address)
                .Set(c => c.BizNumber, updatedCard.BizNumber)
                .Set(c => c.Likes, updatedCard.Likes);

            var result = await _cards.UpdateOneAsync(filter, update);

            // Check if the update was successful
            if (result.MatchedCount == 0)
            {
                throw new CardDoesntExistException(cardId);
            }

            return updatedCard;
        }

        //Get all cards of a specific user
        public async Task<List<Card>> GetUserCardsAsync(string userId)
        {
            var userCards = await _cards.AsQueryable().Where(c => c.User_Id == userId).ToListAsync();
            if(userCards.Count() == 0)
            {
                throw new NoCardsFoundException(userId);
            }
            return userCards;
        }
    }
}
