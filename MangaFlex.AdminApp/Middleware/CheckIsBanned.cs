using MangaFlex.Core.Data.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace MangaFlex.AdminApp.Middleware;

public class CheckIsBannedMiddleware
{
    private readonly RequestDelegate _next;

    public CheckIsBannedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, UserManager<User> _userManager)
    {
        var user = await _userManager.GetUserAsync(httpContext.User);
        if (user == null)
        {
            await _next.Invoke(httpContext);
            return;
        }

        var roles = await _userManager.GetRolesAsync(user);

        if (roles.Contains("Banned") && !httpContext.Request.Path.StartsWithSegments("/ErrorPage/Banned"))
        {
            httpContext.Response.Redirect("/ErrorPage/Banned");
            return;
        }

        if (roles.Contains("User") && !httpContext.Request.Path.StartsWithSegments("/Identity/LogOut"))
        {
            httpContext.Response.Redirect("/Identity/LogOut");
            return;
        }

        await _next.Invoke(httpContext);
    }
}
