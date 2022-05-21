using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

public class CashierController : Controller
{
    public IActionResult Index()
    {        
        return View();
    }
}