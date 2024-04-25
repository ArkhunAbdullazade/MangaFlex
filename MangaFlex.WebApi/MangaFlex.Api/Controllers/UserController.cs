namespace MangaFlex.Api.Controllers;

using MangaFlex.Core.Data.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MangaFlex.Core.Data.Users.Commands;
using System.Drawing;
using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.FriendShip.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MnagaFlex.Api.ViewModels;
using MangaFlex.Api.Dtos;
using MangaFlex.Core.Data.FriendsChat.Commands;
using MangaFlex.Core.Data.Message.Commands;
using MangaFlex.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using System.Text;
using MangaFlex.Core.Data.Message.Models;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly ISender sender;
    private readonly UserManager<User> userManager;
    private readonly IHubContext<ChatHub> hubContext;
    private readonly IHubContext<ProfileHub> hubContextProfile;

    public UserController(ISender sender, UserManager<User> userManager, IHubContext<ChatHub> hubContext, IHubContext<ProfileHub> profilehub)
    {
        this.sender = sender;
        this.userManager = userManager;
        this.hubContext = hubContext;
        this.hubContextProfile = profilehub;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserData(UpdateUserDataDto dto)
    {
        try
        {
            var command = new UpdateUserDataCommand(dto.UserName,dto.OldPassword,dto.Password, userManager.GetUserId(User!));
            await sender.Send(command);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat(string friendid)
    {
        var command = new CreateChatCommand(userManager.GetUserId(User!),friendid);
        await sender.Send(command); 
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage(string message,string friendid)
    {
        Console.WriteLine(212121);
        Console.WriteLine(212121);
        var getchatfirstid = new GetChatIdCommand(userManager.GetUserId(User!), friendid);
        var result = await sender.Send(getchatfirstid);

        var getchatsecondid = new GetChatIdCommand(friendid,userManager.GetUserId(User!));
        var resultsecond = await sender.Send(getchatsecondid);

        var postmessage = new PostMessageCommand(userManager.GetUserId(User!), message, result);
        var postmessagetosecondchat = new PostMessageCommand(userManager.GetUserId(User!), message, resultsecond);

        await sender.Send(postmessage);
        await sender.Send(postmessagetosecondchat);
        await hubContext.Clients.All.SendAsync("ReceiveMessage", "DataChanged");
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Chat(string Id)
    {
        var command = new GetChatCommand(userManager.GetUserId(User!), Id);
        var result = await sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        Console.WriteLine(1);
        var user = HttpContext.User;
        var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine(id);
        var command = new GetUserProfileCommand(user.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> LastWatches()
    {
        var command = new GetUserLastWatchesCommand(userManager.GetUserId(User!));
        var result = await sender.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
    {
        Console.WriteLine(1111);
        using (var image = Image.FromStream(avatar.OpenReadStream()))
        {
            if (image?.Width > 600 || image?.Height > 600)
            {
                ModelState.AddModelError(string.Empty, "Photo cannot be more than 600 x 600 pixels.");
                return View("ChangeAvatar");
            }
        }

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

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAvatar()
    {
        byte[] imageBytes;
        var relativePath = Path.Combine("wwwroot", "uploads", User.Identity.Name + ".png");
        try
        {
            imageBytes = System.IO.File.ReadAllBytes(relativePath);
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return File(imageBytes, "image/jpeg"); 
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAvatarById(string id)
    {
        var user = new GetUserProfileCommand(id);
        var result = await sender.Send(user);

        byte[] imageBytes;
        var relativePath = Path.Combine("wwwroot", "uploads", result.User.UserName + ".png");
        try
        {
            imageBytes = System.IO.File.ReadAllBytes(relativePath);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return File(imageBytes, "image/jpeg");
    }

    [HttpGet]
    public async Task<IActionResult> SearchUser(string search)
    {
        var command = new GetUsersCommand();
        var result = await sender.Send(command);
        return Ok(result.Where(x => x.UserName.Contains(search)));
    }

    [HttpGet]
    public async Task<IActionResult> GetDefaultAvatar()
    {
        byte[] imageBytes;
        var relativePath = Path.Combine("wwwroot", "uploads", "default.png");
        try
        {
            imageBytes = System.IO.File.ReadAllBytes(relativePath);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return File(imageBytes, "image/jpeg");
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var command = new GetUsersCommand();
        var result = await sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ProfileById(string id)
    {
        var command = new GetUserProfileCommand(id);
        var result = await sender.Send(command);
        var subscribtions = await sender.Send(new GetUserSubscriptionsCommand(userManager.GetUserId(User)!));
        result.IsSub = subscribtions.Any(x => x.UserName == result?.User?.UserName);
        result.IsFriends = result.Friends!.Any(x => x.UserName == User?.Identity?.Name) && subscribtions.Any(x => x.UserName == result?.User?.UserName);
        Console.WriteLine(result.Friends!.Any(x => x.UserName == User?.Identity?.Name) && subscribtions.Any(x => x.UserName == result?.User?.UserName));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(string id)
    {
        var command = new SubscribeCommand(userManager.GetUserId(User)!, id);
        await sender.Send(command);
        await hubContextProfile.Clients.All.SendAsync("ReceiveMessage", "DataChanged");
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Unsubscribe(string id)
    {
        var command = new RemoveFriendCommand(userManager.GetUserId(User!), id);
        await sender.Send(command);
        await hubContextProfile.Clients.All.SendAsync("ReceiveMessage", "DataChanged");
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Followers()
    {
        var GetUserSubscriptions = new GetUserSubscriptionsCommand(userManager.GetUserId(User)!);
        var GetUserFollowers = new GetFollowersCommand(userManager.GetUserId(User)!);
        var GetUserFollowersCommandResult = await sender.Send(GetUserFollowers);
        var GetUserSubscriptionsResult = await sender.Send(GetUserSubscriptions);
        return Ok(new FollowersVM()
        {
            Followers = GetUserFollowersCommandResult,
            Subscriptions = GetUserSubscriptionsResult,
        });
    }

}
