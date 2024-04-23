using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.ViewModels;

public class TokenVM
{
    public string jwt {  get; set; }    
    public Guid RefreshToken { get; set; }
}
