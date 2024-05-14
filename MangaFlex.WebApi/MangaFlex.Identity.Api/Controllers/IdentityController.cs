using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MangaFlex.Api.Dto;
using MangaFlex.Api.Dtos;
using MangaFlex.Core.Data.JWT.Options;
using MangaFlex.Core.Data.RefreshToken.Model;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.ViewModels;
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
[AllowAnonymous]
public class IdentityController : ControllerBase
{
    private readonly ISender sender;
    private readonly JwtOptions jwtOptions;
 

    public IdentityController(ISender sender, JwtOptions jwtOptions)
    {
        this.sender = sender;
        this.jwtOptions = jwtOptions;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var result = new TokenVM();
        try
        {
            var command = new SignInCommand(loginDto.Login,loginDto.Password,jwtOptions);
            result = await sender.Send(command);
        }
        catch(ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        return base.Ok(new
        {
            accessToken = result.jwt,
            refreshToken = result.RefreshToken
        });
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateTokenAsync(UpdateTokenDto  updateTokenDto)
    {
        var result = new TokenVM();
        try
        {
            var command = new UpdateTokenCommand(updateTokenDto.AccessToken, updateTokenDto.RefreshToken, jwtOptions);
            result = await sender.Send(command);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new
        {
            accessToken = result.jwt,
            refreshToken = result.RefreshToken
        });
    }

    [HttpPost]
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
            return BadRequest(ex.Message);
        }

        return Created(HttpContext.Request.GetDisplayUrl(),null);
    }
}