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
        // Handle CORS preflight request (OPTIONS)
        if (request.Method == HttpMethod.Options)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            AddCorsHeaders(response, request);
            return response;
        }

        // Handle normal requests
        var httpResponse = await base.SendAsync(request, cancellationToken);

        // Add CORS headers to the response
        AddCorsHeaders(httpResponse, request);

        return httpResponse;
    }

    private void AddCorsHeaders(HttpResponseMessage response, HttpRequestMessage request)
    {
        var origin = request.Headers.GetValues("Origin").FirstOrDefault();

        if (origin != null && allowedOrigins.Contains(origin))
        {
            response.Headers.Add("Access-Control-Allow-Origin", origin);
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
        }
    }
}
