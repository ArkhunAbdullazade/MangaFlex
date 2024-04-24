namespace MangaFlex.Presentation.Controllers;

using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Presentation.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MangaFlex.Core.Data.Users.Commands;
using System.Drawing;
using MangaFlex.Presentation.ViewModels;
using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.FriendShip.Commands;
using Microsoft.AspNetCore.Identity;

[Authorize]
public class UserController : Controller
{
    private readonly ISender sender;
    private readonly UserManager<User> userManager;

    public UserController(ISender sender, UserManager<User> userManager)
    {
        this.sender = sender;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var command = new GetUserProfileCommand(userManager.GetUserId(User!));
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
        var command = new GetUserLastWatchesCommand(userManager.GetUserId(User!));
        var result = await sender.Send(command);
        return View(result);
    }

    [HttpPut]
    public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
    {
        #pragma warning disable CA1416 // Validate platform compatibility
        using (var image = Image.FromStream(avatar.OpenReadStream()))
        {
            if (image?.Width > 600 || image?.Height > 600)
            {
                ModelState.AddModelError(string.Empty, "Photo cannot be more than 600 x 600 pixels.");
                return View("ChangeAvatar");
            }
        }
        #pragma warning restore CA1416 // Validate platform compatibility

        var userName = User?.Identity?.Name;
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

        var command = new ChangeAvatarCommand(userManager.GetUserId(User!), relativePath);
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
            var command = new SignInCommand(dto.Login, dto.Password);
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
            var command = new SignUpCommand(new User()
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
    public async Task<IActionResult> GetUsers()
    {
        var command = new GetUsersCommand();
        var result = await sender.Send(command);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> ProfileById(string id)
    {
        var command = new GetUserProfileCommand(id);
        var result = await sender.Send(command);
        var subscribtions = await sender.Send(new GetUserSubscriptionsCommand(userManager.GetUserId(User)!));
        result.IsSub = subscribtions.Any(x => x.UserName == result?.User?.UserName);
        result.IsFriends = result.Friends!.Any(x => x.UserName == User?.Identity?.Name) && subscribtions.Any(x => x.UserName == result?.User?.UserName);
        return View("Profile", result);
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(string id)
    {
        var command = new SubscribeCommand(userManager.GetUserId(User)!, id);
        await sender.Send(command);
        return RedirectToAction("ProfileById", id);
    }

    [HttpDelete]
    public async Task<IActionResult> Unsubscribe(string id)
    {
        var command = new RemoveFriendCommand(userManager.GetUserId(User!), id);
        await sender.Send(command);
        return base.Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Followers()
    {
        var GetUserSubscriptions = new GetUserSubscriptionsCommand(userManager.GetUserId(User)!);
        var GetUserFollowers = new GetFollowersCommand(userManager.GetUserId(User)!);
        var GetUserFollowersCommandResult = await sender.Send(GetUserFollowers);
        var GetUserSubscriptionsResult = await sender.Send(GetUserSubscriptions);
        return View(new FollowersVM() 
        { 
            Followers = GetUserFollowersCommandResult,
            Subscriptions = GetUserSubscriptionsResult,
        });
    }

}
