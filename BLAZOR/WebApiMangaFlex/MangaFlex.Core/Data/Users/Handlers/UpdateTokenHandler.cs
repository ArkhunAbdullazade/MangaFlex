using MangaFlex.Core.Data.RefreshToken.Command;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

public class UpdateTokenHandler : IRequestHandler<UpdateTokenCommand,TokenVM>
{
    private readonly UserManager<User> userManager;
    private readonly ISender sender;
    
    public UpdateTokenHandler(UserManager<User> userManager, ISender sender)
    {
        this.userManager = userManager;
        this.sender = sender;
    }

    public async Task<TokenVM> Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();

        var validationResult = await handler.ValidateTokenAsync(
            request.Token,
            new TokenValidationParameters()
            {
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(request.JwtOptions.KeyInBytes),

                ValidateAudience = true,
                ValidAudience = request.JwtOptions.Audience,

                ValidateIssuer = true,
                ValidIssuers = request.JwtOptions.Issuers,
            }
        );

        if (validationResult.IsValid == false)
        {
            throw new InvalidOperationException("Token is invalid!");
        }

        var securityToken = handler.ReadJwtToken(request.Token);
        var idClaim = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            throw new InvalidOperationException("JWT Token must contain 'id' claim!");
        }

        var id = idClaim.Value;
        var user = await this.userManager.FindByIdAsync(id);

        if (user == null)
        {
            throw new ArgumentNullException($"Couldn't update the token. User with id '{id}' doesn't exist!");
        }

        var roles = await userManager.GetRolesAsync(user);

        var claims = roles
            .Select(role => new Claim(ClaimTypes.Role, role))
            .Append(new Claim(ClaimTypes.Name, user.UserName))
            .Append(new Claim(ClaimTypes.Email, user.Email))
            .Append(new Claim(ClaimTypes.NameIdentifier, id));

        var securityKey = new SymmetricSecurityKey(request.JwtOptions.KeyInBytes);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var newSecurityToken = new JwtSecurityToken(
            issuer: request.JwtOptions.Issuers.First(),
            audience: request.JwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(request.JwtOptions.LifetimeInMinutes),
            signingCredentials: signingCredentials
        );

        var newJwt = handler.WriteToken(newSecurityToken);

        var commandRefresh = new UpdateRefreshTokenCommand(user.Id, request.RefreshToken);
        var result = await sender.Send(commandRefresh);

        return new TokenVM()
        {
            jwt = newJwt,
            RefreshToken = result,
        };
    }
}

