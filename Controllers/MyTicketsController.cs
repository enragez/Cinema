using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

public class MyTicketsController : Controller
{
    public IActionResult Index()
    {        
        return View();
    }
}