using Application.Chat.Enums;
using Application.Chat.Models;

namespace Application.UnitTests.ChatTests.ModelTests;

[TestFixture]
public class ChatMessageModelTests
{
    [Test]
    public void ChatMessageModel_CreatedAt_ShouldBeSet()
    {
        // Arrange
        ChatMessageModel chatMessage = new();

        // Assert
        Assert.That(chatMessage.CreatedAt, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ChatMessageModel_ModifiedAt_ShouldBeSet()
    {
        // Arrange
        ChatMessageModel chatMessage = new();

        // Assert
        Assert.That(chatMessage.ModifiedAt, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ChatMessageModel_Sender_ShouldBeSet()
    {
        // Arrange
        ChatMessageModel chatMessage = new()
        {
            Sender = ChatSender.Human
        };

        // Assert
        Assert.That(chatMessage.Sender, Is.EqualTo(ChatSender.Human));
    }

    [Test]
    public void ChatMessageModel_Message_ShouldBeSet()
    {
        // Arrange
        ChatMessageModel chatMessage = new()
        {
            Message = "Hello, World!"
        };

        // Assert
        Assert.That(chatMessage.Message, Is.EqualTo("Hello, World!"));
    }

    [Test]
    public void ChatMessageModel_Timestamp_ShouldBeEqualToCreatedAt()
    {
        // Arrange
        ChatMessageModel chatMessage = new();

        // Assert
        Assert.That(chatMessage.Timestamp, Is.EqualTo(chatMessage.CreatedAt));
    }
}
