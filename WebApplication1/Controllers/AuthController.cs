using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class Auth : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}