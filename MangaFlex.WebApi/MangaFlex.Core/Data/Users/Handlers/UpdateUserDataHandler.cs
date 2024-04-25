using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

internal class UpdateUserDataHandler : IRequestHandler<UpdateUserDataCommand>
{
    private readonly IUserRepository userRepository;

    public UpdateUserDataHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        await userRepository.UpdateUserData(request.UserName, request.OldPassword, request.NewPassword, request.UserId);
    }
}
