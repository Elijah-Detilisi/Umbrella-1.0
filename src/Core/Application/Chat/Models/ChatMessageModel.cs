using Application.Chat.Enums;

namespace Application.Chat.Models;

public class ChatMessageModel : Model
{
    public ChatSender Sender { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp => CreatedAt;
}
