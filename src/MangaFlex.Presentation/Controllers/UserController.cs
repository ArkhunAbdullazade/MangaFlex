namespace MangaFlex.Presentation.Controllers;

using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Presentation.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MangaFlex.Core.Data.Users.Commands;
using System.Security.Claims;
using System.Drawing;

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
    public IActionResult Settings()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ChangeAvatar()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> LastWatches()
    {
        var command = new GetUserLastWatchesCommand(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await sender.Send(command);
        return View(result);
    }

    [HttpPut]
    public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
    {
        using (var image = Image.FromStream(avatar.OpenReadStream()))
        {
            if (image.Width > 600 || image.Height > 600)
            {
                ModelState.AddModelError(string.Empty, "Photo cannot be more than 600 x 600 pixels.");
                return View("ChangeAvatar");
            }
        }

        var userName = User.Identity.Name;
        var fileName = $"{userName}{Path.GetExtension(avatar.FileName)}";

        var destinationFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        var destinationPath = Path.Combine(destinationFolder, fileName);

        if (System.IO.File.Exists(destinationPath))
        {
            System.IO.File.Delete(destinationPath);
        }

        Directory.CreateDirectory(destinationFolder);

        using (var stream = new FileStream(destinationPath, FileMode.Create))
        {
            await avatar.CopyToAsync(stream);
        }
        var relativePath = "/uploads/" + fileName;

        var command = new ChangeAvatarCommand(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!, relativePath);
        await sender.Send(command);

        return RedirectToAction("Profile");
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

    [HttpGet]
    public async Task<IActionResult> GetAnotherUsers()
    {
        var command = new GetAnotherUserCommand();
        var result = await sender.Send(command);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> ProfileById(string id)
    {
        var command = new GetUserProfileCommand(id);
        var result = await sender.Send(command);
        var userfriends = await sender.Send(new GetUserFriendsCommand(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!));
        result.IsSub = userfriends.Any(x => x.UserName == result.User.UserName);
        result.IsFriends = (result.Friends.Any(x => x.UserName == User.Identity.Name) && userfriends.Any(x => x.UserName == result.User.UserName));
        return View("Profile",result);
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(string id)
    {
        var command = new SubscribeCommand(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!,id);
        await sender.Send(command);
        return RedirectToAction("ProfileById", id);
    }
}
