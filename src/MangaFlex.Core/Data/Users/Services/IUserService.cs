namespace MangaFlex.Core.Data.Users.Services;

using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Models.ManyToMany;

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
    public Task UpdateAvatarAsync(string url, string id);
    public Task AddFriendAsync(string userid, string friendid);
    public Task RemoveFriendAsync(string userid,string friendid);
    public Task<IEnumerable<User>> GetAllFriendsAsync(string userid);
    public Task<IEnumerable<User>> GetAnotherPeopleAsync();
}
