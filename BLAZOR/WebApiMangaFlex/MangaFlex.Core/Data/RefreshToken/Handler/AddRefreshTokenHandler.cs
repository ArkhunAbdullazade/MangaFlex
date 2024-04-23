using MangaFlex.Core.Data.RefreshToken.Command;
using MangaFlex.Core.Data.RefreshToken.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.RefreshToken.Handler;

public class AddRefreshTokenHandler : IRequestHandler<AddRefreshTokenCommand, Guid>
{
    private readonly IRefreshTokenRepository refreshTokenRepository;

    public AddRefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        this.refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Guid> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await refreshTokenRepository.AddRefreshTokenAsync(request.UserId);
        return result;
    }
}
