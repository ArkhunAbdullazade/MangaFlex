using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Models.ManyToMany;

public class FriendShip
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }

    public string FriendId { get; set; }
    public User Friend { get; set; }
}