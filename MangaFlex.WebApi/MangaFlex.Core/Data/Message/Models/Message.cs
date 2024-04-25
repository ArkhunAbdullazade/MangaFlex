using MangaFlex.Core.Data.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Message.Models;

public class Message
{
    public int Id { get; set; }
    public int ChatId {  get; set; }
    public string MessageContent {  get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public string UserId {  get; set; }
    public User User {  get; set; }
}
