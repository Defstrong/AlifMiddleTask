using System.Net;
using System.Net.Mime;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace Presentation;

public class AuthFilter : ActionFilterAttribute
{
    private IConfiguration _configuration;
    public AuthFilter(IConfiguration configuration)
        => _configuration = configuration;
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        string? userId = context.HttpContext.Request.Headers["X-UserId"].FirstOrDefault();
        string? digest = context.HttpContext.Request.Headers["X-Digest"].FirstOrDefault();
        string? secretKey = _configuration.GetValue<string>("SecretKey");

        if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(digest))
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Content = "Missing X-UserId or X-Digest"
            };
            return ;
        }
        if(string.IsNullOrEmpty(secretKey))
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Internal Server Error: Missing secret key in configuration"
            };
            return ;
        }

        string requestBody = new System.IO.StreamReader(context.HttpContext.Request.Body).ReadToEnd();
        string calculateDigest = ComputeHMACSHA1(requestBody, secretKey);

        if(calculateDigest != digest)
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Content = "Invalid X-Digest"
            };
            return;
        }
    }

    private string ComputeHMACSHA1(string input, string secretKey)
    {
        using HMACSHA1 hmac = new (Encoding.UTF8.GetBytes(secretKey));
        byte[]? hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hash);
    }
}