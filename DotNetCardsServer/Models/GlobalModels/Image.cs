namespace DotNetCardsServer.Models.Users
{
    public class Image
    {
        public string Url { get; set; }
        public string? Alt { get; set; }

        public Image(string url, string alt)
        {
            Url = url;
            Alt = alt;
        }
    }
}
