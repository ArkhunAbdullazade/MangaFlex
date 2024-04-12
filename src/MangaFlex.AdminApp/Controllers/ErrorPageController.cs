using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFlex.AdminApp.Controllers;

public class ErrorPageController : Controller
{
    public ErrorPageController() { }

    [HttpGet]
    public IActionResult Banned()
    {
        return View();
    }
}
