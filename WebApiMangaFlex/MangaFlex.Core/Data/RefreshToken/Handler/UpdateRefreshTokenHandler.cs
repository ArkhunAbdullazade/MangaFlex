using MangaFlex.Core.Data.RefreshToken.Command;
using MangaFlex.Core.Data.RefreshToken.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.RefreshToken.Handler;

public class UpdateRefreshTokenHandler : IRequestHandler<UpdateRefreshTokenCommand, Guid>
{
    private readonly IRefreshTokenRepository refreshTokenRepository;

    public UpdateRefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        this.refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Guid> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await refreshTokenRepository.UpdateRefreshTokenAsync(request.Token,request.UserId);
        return result;
    }
}
