namespace MangaFlex.Core.Data.User.Services;

using MangaFlex.Core.Data.User.Models;

public interface IUserService
{
    public Task LoginAsync(string userName, string password);
    public Task SignupAsync(User user, string password);
    public Task SignOutAsync();
}
