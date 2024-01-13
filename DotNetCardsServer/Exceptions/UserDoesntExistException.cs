namespace DotNetCardsServer.Exceptions
{
    public class UserDoesntExistException : Exception
    {
        public UserDoesntExistException(string message) : base(message) { }
    }
}
