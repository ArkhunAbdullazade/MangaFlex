using MangaFlex.Core.Data.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendShip.Repository;

public interface IFriendShipRepository
{
    public Task AddFriendAsync(string userid, string friendid);
    public Task RemoveFriendAsync(string userid, string friendid);
    public Task<IEnumerable<User>> GetSubscriptionsAsync(string userid);
    public Task<IEnumerable<User>> GetFollowersAsync(string userid);
}
