namespace MangaFlex.Core.Data.Users.Services;

using MangaFlex.Core.Data.Users.Models;

public interface IUserService
{
    public Task<User> GetByIdAsync(string id);
    public Task DeleteLastWatchAsync(string userid, string mangaid);
    public Task<bool> IsMangaInLastWatchAsync(string userid, string mangaid);
    public Task<IEnumerable<LastWatch>> GetLastWatchAsync(string userId);
    public Task AddLastWatchAsync(string mangaid, string userid);
    public Task LoginAsync(string userName, string password);
    public Task SignupAsync(User user, string password);
    public Task SignOutAsync();
    public Task UpdateAvatar(string url, string id);
}
