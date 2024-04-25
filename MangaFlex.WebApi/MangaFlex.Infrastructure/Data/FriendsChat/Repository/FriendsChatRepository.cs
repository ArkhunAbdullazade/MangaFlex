namespace MangaFlex.Infrastructure.Data.FriendsChat.Repository;

using MangaFlex.Core.Data.FriendsChat.Repository;
using MangaFlex.Core.Data.Message.Models;
using MangaFlex.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FriendsChatRepository : IFriendsChatRepository
{
    private readonly MangaFlexDbContext mangaFlexDbContext;

    public FriendsChatRepository(MangaFlexDbContext mangaFlexDbContext)
    {
        this.mangaFlexDbContext = mangaFlexDbContext;
    }

    public async Task<int> GetChatId(string userid, string friendid)
    {
        var chat =  await mangaFlexDbContext.FriendsChats.FirstOrDefaultAsync(x => x.UserId == userid && x.FriendId == friendid);
        return chat.Id;
    }

    public async Task<IEnumerable<Message>> GetMessagesInChat(string userid, string friendid)
    {
        var chat = await mangaFlexDbContext.FriendsChats.FirstOrDefaultAsync(x => x.UserId == userid);
        var messages =  mangaFlexDbContext.Messages.Where(x => x.ChatId == chat.Id);

        return messages;
    }
}
