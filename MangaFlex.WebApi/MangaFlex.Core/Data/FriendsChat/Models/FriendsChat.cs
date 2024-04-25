namespace MangaFlex.Core.Data.FriendsChat.Models;

using MangaFlex.Core.Data.Message.Models;
using MangaFlex.Core.Data.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FriendsChat
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string FriendId { get; set; }
    public User Friend { get; set; }
    public Guid ChatId { get; set; } = Guid.NewGuid();
    public ICollection<Message> Messages {  get; set; } 
}
