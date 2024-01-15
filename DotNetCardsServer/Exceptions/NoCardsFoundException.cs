namespace DotNetCardsServer.Exceptions
{
    public class NoCardsFoundException : Exception
    {
        public NoCardsFoundException(string userId)
            : base($"No cards were found for user with ID '{userId}'.") { }
    }
}
