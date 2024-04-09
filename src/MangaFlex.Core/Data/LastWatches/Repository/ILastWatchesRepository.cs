using MangaFlex.Core.Data.LastWatches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.LastWatches.Repository;

public interface ILastWatchesRepository
{
    public Task DeleteLastWatchAsync(string userid, string mangaid);
    public Task<bool> IsMangaInLastWatchAsync(string userid, string mangaid);
    public Task<IEnumerable<LastWatch>> GetLastWatchAsync(string userId);
    public Task AddLastWatchAsync(string mangaid, string userid);
}
