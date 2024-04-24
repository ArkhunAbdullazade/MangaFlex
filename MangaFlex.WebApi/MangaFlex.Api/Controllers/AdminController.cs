using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MangaFlex.Api.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class AdminController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ISender sender;

    public AdminController(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ISender sender)
    {
        this.userRepository = userRepository;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.sender = sender;
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> Ban(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        var existingRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, existingRoles);
        var role = new IdentityRole { Name = "Banned" };
        await roleManager.CreateAsync(role);
        await userManager.AddToRoleAsync(user, role.Name);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SetModerator(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        var existingRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, existingRoles);
        var role = new IdentityRole { Name = "Moderator" };
        await roleManager.CreateAsync(role);
        await userManager.AddToRoleAsync(user, role.Name);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveModerator(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        var existingRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, existingRoles);
        await userManager.AddToRoleAsync(user, "User");
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Moderator")]
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
        return Ok();
    }

}
