using DotNetCardsServer.Exceptions;
using DotNetCardsServer.Models.Cards;
using DotNetCardsServer.Services.Cards;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetCardsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private CardsService _cardsService;

        public CardController(IMongoClient mongoClient)
        {
            _cardsService = new CardsService(mongoClient);
        }

        //Get all cards
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Card> result = await _cardsService.GetCardsAsync();
            return Ok(result);
        }

        // GET specific card
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string cardId)
        {
            try
            {
                Card requestedCard = await _cardsService.GetCardAsync(cardId);
                return Ok(requestedCard);
            } catch (CardDoesntExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUserCards(string userId)
        {
            try
            {
                var cards = await _cardsService.GetUserCardsAsync(userId);
                return Ok(cards);
            } catch(NoCardsFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CardController>
        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromBody] Card newCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _cardsService.CreateCardAsync(newCard);
            }
            catch (CardAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Get), new { Id = newCard._id }, newCard);
        }

        // PUT api/<CardController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Card updatedCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Card newCard = await _cardsService.EditCardAsync(id, updatedCard);
            }
            catch (CardDoesntExistException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        // DELETE api/<CardController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _cardsService.DeleteCardAsync(id);
            }
            catch (UserDoesntExistException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
    }
}
