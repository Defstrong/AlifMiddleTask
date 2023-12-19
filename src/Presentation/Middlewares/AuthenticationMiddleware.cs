using System.Net;
using System.Text;
using System.Security.Cryptography;
using BusinessLogic;

namespace Presentation;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKeyHelper = "SecretKey";

    public AuthenticationMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context, IClientService clientService, IConfiguration configuration)
    {
        string? userId = context.Request.Headers["X-UserId"].FirstOrDefault();
        string? digest = context.Request.Headers["X-Digest"].FirstOrDefault();
        string? secretKey = configuration.GetValue<string>(_secretKeyHelper);

        ArgumentException.ThrowIfNullOrEmpty(secretKey);

        context.Request.EnableBuffering();

        using StreamReader reader = new (context.Request.Body, Encoding.UTF8, true, 1024, true);

        string requestBody = await reader.ReadToEndAsync();
        context.Request.Body.Seek(0, SeekOrigin.Begin);

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(digest))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Missing X-UserId or X-Digest");
            return ;
        }

        if(!await clientService.CheckAsync(userId))
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync("User not found");
            return ;
        }

        string calculatedDigest = ComputeHMACSHA1(requestBody, secretKey);

        if (calculatedDigest != digest)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Invalid X-Digest");
            return ;
        }

        await _next(context);
    }

    // A method for hashing with a special key that is known to both parties.
    private static string ComputeHMACSHA1(string data, string key)
    {
        using HMACSHA1 hmac = new (Encoding.UTF8.GetBytes(key));
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hashBytes);
    }
}
