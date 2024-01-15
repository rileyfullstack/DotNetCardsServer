namespace DotNetCardsServer.Exceptions
{
    public class CardDoesntExistException : Exception
    {
        public CardDoesntExistException(string message) : base(message) { }
    }
}
