using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Message.Commands;

public class PostMessageCommand : IRequest
{
    public string UserId {  get; set; }
    public string Message { get; set; }
    public int ChatId {  get; set; }
    public PostMessageCommand(string userId, string message, int chatId)
    {
        UserId = userId;
        Message = message;
        ChatId = chatId;
    }
}
