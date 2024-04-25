using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Commands;

public class UpdateUserDataCommand : IRequest
{
    public string UserName {  get; set; }
    public string OldPassword {  get; set; }
    public string NewPassword { get; set; }
    public string UserId {  get; set; }
    public UpdateUserDataCommand(string userName, string oldPassword, string newPassword, string userId)
    {
        UserName = userName;
        OldPassword = oldPassword;
        NewPassword = newPassword;
        UserId = userId;
    }
}
