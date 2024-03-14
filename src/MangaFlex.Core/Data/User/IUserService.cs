using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.User;

public interface IUserService
{
    public Task LoginAsync(string userName, string password);
    public Task SignupAsync(User user, string password);
    public Task SignOutAsync();
}
