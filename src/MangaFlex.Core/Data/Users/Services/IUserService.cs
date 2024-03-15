namespace MangaFlex.Core.Data.Users.Services;

using MangaFlex.Core.Data.Users.Models;

public interface IUserService
{
    public Task LoginAsync(string userName, string password);
    public Task SignupAsync(User user, string password);
    public Task SignOutAsync();
}
