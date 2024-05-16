using System.Diagnostics;
using MangaFlex.AdminApp.Dto;
using MangaFlex.AdminApp.Models;
using MangaFlex.AdminApp.ViewModels;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MangaFlex.AdminApp.Controllers;


[Authorize(Roles ="Admin,Moderator")]
public class HomeController : Controller
{
    private readonly IUserRepository userRepository;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ISender sender;

    public HomeController(IUserRepository userRepository, UserManager<User> userManager,RoleManager<IdentityRole> roleManager, ISender sender)
    {
        this.userRepository = userRepository;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.sender = sender;
    }

    [HttpPut]
    public async Task<IActionResult> Ban(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        var existingRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, existingRoles);
        var role = new IdentityRole { Name = "Banned" };
        await roleManager.CreateAsync(role);
        await userManager.AddToRoleAsync(user, role.Name);
        return RedirectToAction("ShowAllUsers");
    }

    [HttpPut]
    public async Task<IActionResult> SetModerator(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        var existingRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, existingRoles);
        var role = new IdentityRole { Name = "Moderator" };
        await roleManager.CreateAsync(role);
        await userManager.AddToRoleAsync(user, role.Name);
        return RedirectToAction("ShowAllUsers");
    }

    [HttpPut]
    public async Task<IActionResult> RemoveModerator(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        var existingRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, existingRoles);
        await userManager.AddToRoleAsync(user, "User");
        return RedirectToAction("ShowAllUsers");
    }

    [HttpPut]
    public async Task<IActionResult> UnBan(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new NullReferenceException($"User not found by id {id}");
        }
        var existingRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, existingRoles);
        await userManager.AddToRoleAsync(user, "User");
        return RedirectToAction("ShowAllUsers");
    }

    [HttpGet]
    public async Task<IActionResult> ShowAllUsers()
    {
        var users = await userRepository.GetUsersAsync();
        var model = new List<ShowAllUsersVM>();
        foreach (var user in users)
        {
            var rolelist = await userManager.GetRolesAsync(user);
            var role =rolelist.FirstOrDefault();
            model.Add(new ShowAllUsersVM()
            {
                User = user,
                Role = role,
            });
        }
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
