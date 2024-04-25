using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using MangaFlexFront.Data.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace MangaFlexFront.Providers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly JwtSecurityTokenHandler jwtTokenHandler;
        private readonly ILocalStorageService localStorageService;

        public CustomAuthenticationStateProvider(
            ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
            this.jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(string? jwt,string? refreshToken)
        {
            Console.WriteLine(jwt);
            if (string.IsNullOrWhiteSpace(jwt))
            {
                return new ClaimsIdentity();
            }

            var validationResult = await jwtTokenHandler.ValidateTokenAsync(
                jwt,
                new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = "MangaFlex",

                    ValidateIssuer = true,
                    ValidIssuer = "MangaFlex.Inc",

                    SignatureValidator = (token, validationParameters) => new JwtSecurityToken(token),

                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires > DateTime.UtcNow,
                }
            );

            if (validationResult.IsValid == false)
            {
                if (validationResult.Exception is SecurityTokenInvalidLifetimeException lifetimeException)
                {
                    try
                    {
                        var httpClient = new HttpClient();

                        var updateTokenResponse = await httpClient.PutAsJsonAsync("http://localhost:5262/api/Identity/UpdateToken", new { AccessToken = jwt, RefreshToken = refreshToken });

                        Console.WriteLine(updateTokenResponse.IsSuccessStatusCode);
                        if (updateTokenResponse.IsSuccessStatusCode && updateTokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var tokenResponse = await updateTokenResponse.Content.ReadFromJsonAsync<TokenVM>();

                            var accessToken = tokenResponse!.accessToken;
                            var refreshTokenupdate = tokenResponse!.refreshToken;
                            Console.WriteLine(accessToken);
                            Console.WriteLine(refreshTokenupdate);
                            await this.localStorageService.SetItemAsStringAsync("accessToken", accessToken);
                            await this.localStorageService.SetItemAsStringAsync("refreshToken", refreshTokenupdate.ToString());

                            var newToken = jwtTokenHandler.ReadJwtToken(accessToken);

                            return new ClaimsIdentity(newToken.Claims, "jwt");
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                  
                }

                return new ClaimsIdentity();
            }

            var token = jwtTokenHandler.ReadJwtToken(jwt);

            return new ClaimsIdentity(token.Claims, "jwt");
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine(1);
            var jwt = await this.localStorageService.GetItemAsStringAsync("accessToken");
            var refreshtoken = await this.localStorageService.GetItemAsStringAsync("refreshToken");

            Console.WriteLine(jwt);

            var claimsIdentity = await this.GetClaimsIdentityAsync(jwt,refreshtoken);

            Console.WriteLine(claimsIdentity.Name);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            Console.WriteLine(claimsPrincipal.Identity.Name);
            Console.WriteLine(claimsPrincipal.IsInRole("User"));

            var authenticationState = new AuthenticationState(claimsPrincipal);

            base.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));

            return authenticationState;
        }        

    }
}