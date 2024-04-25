using MangaFlex.Core.Data.Message.Repository;
using MangaFlex.Infrastructure.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Infrastructure.Data.Message.Repository;

using MangaFlex.Core.Data.Message.Models;

public class MessageRepository : IMessageRepository
{
    private MangaFlexDbContext dbContext;

    public MessageRepository(MangaFlexDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task PostMessage(string UserId, string Message, int ChatId)
    {
        await dbContext.Messages.AddAsync(new Message()
        {
            UserId = UserId,
            ChatId = ChatId,
            MessageContent = Message
        });
        await dbContext.SaveChangesAsync();
    }
}
