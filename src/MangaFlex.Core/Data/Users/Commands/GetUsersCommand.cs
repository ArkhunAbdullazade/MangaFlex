using MangaFlex.Core.Data.Users.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Commands;

public class GetUsersCommand : IRequest<IEnumerable<User>>
{
}
