namespace DotNetCardsServer.Models.Users
{
    public class Image
    {
        string Url { get; set; }
        string? Alt { get; set; }

        public Image(string url, string alt)
        {
            Url = url;
            Alt = alt;
        }
    }
}
