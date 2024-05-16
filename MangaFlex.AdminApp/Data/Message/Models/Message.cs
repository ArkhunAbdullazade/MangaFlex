
using MangaFlexFront.Data.Users.Models;

namespace MangaFlexFront.Data.Message.Models;

public class Message
{
    public int Id { get; set; }
    public int ChatId {  get; set; }
    public string MessageContent {  get; set; }
    public DateTime SentAt { get; set; } = DateTime.Now;
    public string UserId {  get; set; }
    public User User {  get; set; }
}
