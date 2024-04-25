using MangaFlex.Core.Data.Message.Commands;
using MangaFlex.Core.Data.Message.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Message.Handlers;

public class PostMessageHandler : IRequestHandler<PostMessageCommand>
{
    private IMessageRepository messageRepository;
    public PostMessageHandler(IMessageRepository messageRepository)
    {
        this.messageRepository = messageRepository;
    }

    public async Task Handle(PostMessageCommand request, CancellationToken cancellationToken)
    {
        await messageRepository.PostMessage(request.UserId, request.Message, request.ChatId);
    }
}
