using MangaFlex.Core.Data.User;
using MangaFlex.Presentation.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MangaFlex.Presentation.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm]LoginDto dto)
    {
        await _userService.LoginAsync(dto.Login, dto.Password);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Registration()
    {
        return View();
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Registration([FromForm] RegistrationDto dto)
    {
        try
        {
            await _userService.SignupAsync(new User()
            {
                UserName = dto.Login,
                Email = dto.Email,
            }, dto.Password);
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
}
