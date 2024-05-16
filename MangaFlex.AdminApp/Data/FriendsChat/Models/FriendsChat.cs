namespace MangaFlex.Core.Data.FriendShip.Models;

using MangaFlexFront.Data.Message.Models;
using MangaFlexFront.Data.Users.Models;
using System;
using System.Collections.Generic;

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
