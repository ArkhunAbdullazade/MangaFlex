using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MangaFlex.Api.Dto;
using MangaFlex.Api.Options;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MangaFlex.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class IdentityController : ControllerBase
{
    private readonly JwtOptions jwtOptions;
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    private readonly ISender sender;

    public IdentityController(IOptionsSnapshot<JwtOptions> JwtOptions, SignInManager<User> signInManager, UserManager<User> userManager,ISender sender)
    {
        this.jwtOptions = JwtOptions.Value;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var user = await this.userManager.FindByNameAsync(loginDto.Login!);

        if (user is null)
        {
            return base.BadRequest(new
            {
                Message = "Incorrect login or password",
            });
        }

        var signInResult = await this.signInManager.PasswordSignInAsync(user, loginDto.Password!, false, false);

        if (signInResult.IsLockedOut)
        {
            return base.BadRequest(new
            {
                Message = "Account was locked",
            });
        }

        if (signInResult.Succeeded == false)
        {
            return base.BadRequest(new
            {
                Message = "Incorrect login or password",
            });
        }

        var roles = await userManager.GetRolesAsync(user);

        var claims = roles
         .Select(role => new Claim(ClaimTypes.Role, role))
         .Append(new Claim(ClaimTypes.Name, loginDto.Login))
         .Append(new Claim(ClaimTypes.NameIdentifier, user.Id));


        var securityKey = new SymmetricSecurityKey(this.jwtOptions.KeyInBytes);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
           issuer: this.jwtOptions.Issuers.First(),
           audience: this.jwtOptions.Audience,
           claims,
           expires: DateTime.Now.AddMinutes(this.jwtOptions.LifetimeInMinutes),
           signingCredentials: signingCredentials
        );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwt = jwtSecurityTokenHandler.WriteToken(securityToken);

        return base.Ok(jwt);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegistrationAsync(RegistrationDto dto)
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
        catch (AggregateException ex)
        {
            return BadRequest(ex);
        }

        return Created(HttpContext.Request.GetDisplayUrl(),null);
    }
}