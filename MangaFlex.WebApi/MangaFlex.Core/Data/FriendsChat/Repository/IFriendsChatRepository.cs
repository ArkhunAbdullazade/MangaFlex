namespace MangaFlex.Core.Data.FriendsChat.Repository;

using MangaFlex.Core.Data.Message.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IFriendsChatRepository
{

    public Task CreateChat(string userid, string friendid);
    public Task<IEnumerable<Message>> GetMessagesInChat(string userid, string friendid);
    public Task<int> GetChatId(string userid, string friendid);

}
