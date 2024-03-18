namespace MangaFlex.Presentation.Controllers;

using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Presentation.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MangaFlex.Core.Data.Users.Commands;
using System.Security.Claims;

[Authorize]
public class UserController : Controller
{
    private readonly ISender sender;

    public UserController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var command = new GetUserProfileCommand(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await sender.Send(command);
        return View(result);
    }



    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm]LoginDto dto)
    {
        try
        {
            var command = new LoginInCommand(dto.Login, dto.Password);
            await sender.Send(command);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(ex.Source!, ex.Message);
            return View("Login");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Registration() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Registration([FromForm] RegistrationDto dto)
    {
        try
        {
            var command = new SignInCommand(new User()
            {
                UserName = dto.Login,
                Email = dto.Email,
            }, dto.Password);
            await sender.Send(command);
        }
        catch(AggregateException ex)
        {
            foreach (ArgumentException error in ex.Flatten().InnerExceptions)
            {
                base.ModelState.AddModelError(error.ParamName!, error.Message);
            }
            return View("Registration");
        }
       
        return RedirectToAction("Login");
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        var command = new SignOutCommand();
        await sender.Send(command);
        return RedirectToAction("Index", "Home");
    }
}
