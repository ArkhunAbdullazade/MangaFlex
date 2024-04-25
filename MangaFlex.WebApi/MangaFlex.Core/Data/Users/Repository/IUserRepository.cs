namespace MangaFlex.Core.Data.Users.Repository;

using MangaFlex.Core.Data.Users.Models;

public interface IUserRepository
{
    public Task<User> GetByIdAsync(string id);  
    public Task UpdateAvatarAsync(string url, string id);  
    public Task<IEnumerable<User>> GetUsersAsync();
    public Task UpdateUserData(string nickname,string oldpassword,string newpassword, string id);
}
