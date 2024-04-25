using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Message.Repository;

public interface IMessageRepository
{
    public Task PostMessage(string UserId, string Message, int ChatId);
}
