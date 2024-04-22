using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.RefreshToken.Repository;

public interface IRefreshTokenRepository
{
    public Task<Guid> AddRefreshTokenAsync(string UserId);
    public Task<Guid> UpdateRefreshTokenAsync(Guid Token, string UserId);

}
