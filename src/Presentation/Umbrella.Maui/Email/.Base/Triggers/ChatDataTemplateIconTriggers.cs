using Umbrella.Maui.Email.Base.Enum;

namespace Umbrella.Maui.Email.Base.Triggers;

public static class ChatDataTemplateIconTriggers
{
    //Data triggers
    public static DataTrigger HumanSenderTrigger => new(typeof(Image))
    {
        Value = ChatSender.Human,
        Binding = new Binding(nameof(ChatMessageModel.Sender)),
        Setters =
        {
            new Setter()
            {
                Property = Grid.ColumnProperty,
                Value = (int)ChatTemplateColumn.Left // Go left for human sender
            }
        }
    };

    public static DataTrigger BotSenderTrigger => new(typeof(Image))
    {
        Value = ChatSender.Bot,
        Binding = new Binding(nameof(ChatMessageModel.Sender)),
        Setters =
        {
            new Setter()
            {
                Property = Grid.ColumnProperty,
                Value = (int)ChatTemplateColumn.Right // Go right for human sender
            }
        }
    };
}
