using Microsoft.AspNetCore.Mvc;

namespace MeuCantinhoCriativo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
