using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {        
        return View();
    }
}