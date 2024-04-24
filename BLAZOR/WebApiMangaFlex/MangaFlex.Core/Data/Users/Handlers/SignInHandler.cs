namespace MangaFlex.Core.Data.Users.Handlers;

using MangaFlex.Core.Data.JWT.Options;
using MangaFlex.Core.Data.RefreshToken.Command;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

public class SignInHandler : IRequestHandler<SignInCommand, TokenVM>
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    private readonly ISender sender;
    public SignInHandler(UserManager<User> userManager, SignInManager<User> signInManager, ISender sender)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.sender = sender;
    }
    public async Task<TokenVM> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByNameAsync(request.Login!);

        if (user is null)
        {
            throw new ArgumentNullException("Incorrect login or password");
        }

        var signInResult = await this.signInManager.PasswordSignInAsync(user!, request.Password!, false, true);

        if (signInResult.IsLockedOut)
        {
            throw new InvalidOperationException("The account is locked due to too many failed login attempts. Please try again later.");
        }

        if (signInResult.Succeeded == false)
        {
            throw new InvalidOperationException("Incorrect login or password");
        }

        var roles = await userManager.GetRolesAsync(user!);

        var claims = roles
         .Select(role => new Claim(ClaimTypes.Role, role))
         .Append(new Claim(ClaimTypes.Name, request.Login!))
         .Append(new Claim(ClaimTypes.Email, user.Email))
         .Append(new Claim(ClaimTypes.NameIdentifier, user.Id!));


        var securityKey = new SymmetricSecurityKey(request.Jwt.KeyInBytes);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
           issuer: request.Jwt.Issuers.First(),
           audience: request.Jwt.Audience,
           claims,
           expires: DateTime.Now.AddMinutes(request.Jwt.LifetimeInMinutes),
           signingCredentials: signingCredentials
        );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwt = jwtSecurityTokenHandler.WriteToken(securityToken);

        var command = new AddRefreshTokenCommand(user.Id);
        var result = await sender.Send(command);

        return new TokenVM()
        {
            jwt = jwt,
            RefreshToken = result,
        };
    }
}
