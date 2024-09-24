using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

public class CorsMessageHandler : DelegatingHandler
{
    private readonly string[] allowedOrigins = new string[]
    {
        "http://localhost:4200",
        "https://ztanvir-1.github.io"
    };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var origin = request.Headers.GetValues("Origin").FirstOrDefault();

        // Handle preflight OPTIONS request
        if (request.Method == HttpMethod.Options && origin != null && allowedOrigins.Contains(origin))
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", origin);
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
            return response;
        }

        // Handle normal requests
        var httpResponse = await base.SendAsync(request, cancellationToken);

        // Add CORS headers to the response for normal requests
        if (origin != null && allowedOrigins.Contains(origin))
        {
            httpResponse.Headers.Add("Access-Control-Allow-Origin", origin);
            httpResponse.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            httpResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
            httpResponse.Headers.Add("Access-Control-Allow-Credentials", "true");
        }

        return httpResponse;
    }
}
