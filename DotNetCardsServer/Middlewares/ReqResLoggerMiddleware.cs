namespace DotNetCardsServer.Middlewares
{
    public class ReqResLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public ReqResLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //do something with the request
            Console.WriteLine("Hello from my middleware");

            await _next(context);

            //do something with the response (after it was sent)
        }
    }

}
