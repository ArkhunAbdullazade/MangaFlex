using MangaFlex.AdminApp.Dto;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MangaFlex.AdminApp.Controllers;

public class IdentityController : Controller
{
    private readonly ISender sender;
    private readonly UserManager<User> userManager;

    public IdentityController(ISender sender, UserManager<User> userManager)
    {
        this.sender = sender;
        this.userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] LoginDto dto)
    {
        try
        {
            var command = new SignInCommand(dto.Login, dto.Password);
            await sender.Send(command);
            var user = await userManager.FindByNameAsync(dto.Login);
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Contains("User"))
            {
                ModelState.AddModelError("ModalError", "You are not an administrator or moderator");
                return View("Login");
            }
            return RedirectToAction("Home", "Identity");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(ex.Source!, ex.Message);
            return View("Login");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> LogOut()
    {
        var command = new SignOutCommand();
        await sender.Send(command);
        return RedirectToAction("Home", "Identity");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Home()
    {
        return View();
    }
}
