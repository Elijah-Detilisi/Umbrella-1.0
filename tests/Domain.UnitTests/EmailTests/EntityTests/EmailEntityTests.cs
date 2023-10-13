namespace Domain.UnitTests.EmailTests.EntityTests;

[TestFixture]
public class EmailEntityTests
{
    [Test]
    public void Create_NewEmailEntity_ShouldSetProperties()
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
        var emailEntity = EmailEntity.Create(recipients, subject, body);

        // Assert
        Assert.That(emailEntity, Is.Not.Null);
        Assert.That(emailEntity.Recipients, Is.SameAs(recipients));
        Assert.That(emailEntity.Subject, Is.EqualTo(subject));
        Assert.That(emailEntity.Body, Is.EqualTo(body));
    }
}
