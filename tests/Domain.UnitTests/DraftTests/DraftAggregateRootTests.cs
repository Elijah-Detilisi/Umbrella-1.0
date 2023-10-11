using Domain.Draft;

namespace Domain.UnitTests.DraftTests;

[TestFixture]
public class DraftAggregateRootTests
{
    [Test]
    public void Create_NewDraft_ShouldSetProperties()
    {
        // Arrange
        List<EmailAddress> recipients = new List<EmailAddress>
            {
                EmailAddress.Create("recipient1@example.com"),
                EmailAddress.Create("recipient2@example.com")
            };
        EmailSubjectLine subject = EmailSubjectLine.Create("Test Subject");
        EmailBodyText body = EmailBodyText.Create("This is the email body.");

        // Act
        DraftAggregateRoot draft = DraftAggregateRoot.Create(recipients, subject, body);

        // Assert
        Assert.That(draft, Is.Not.Null);
        Assert.That(draft.Recipients, Is.SameAs(recipients));
        Assert.That(draft.Subject, Is.EqualTo(subject));
        Assert.That(draft.Body, Is.EqualTo(body));
    }
}