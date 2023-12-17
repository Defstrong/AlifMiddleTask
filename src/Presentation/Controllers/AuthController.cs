using Microsoft.AspNetCore.Mvc;
namespace Presentation;

[ServiceFilter(typeof(AuthFilter))]
public class AuthController : ControllerBase
{
    
}