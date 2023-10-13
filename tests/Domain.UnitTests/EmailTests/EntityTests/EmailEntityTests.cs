namespace Domain.UnitTests.EmailTests.EntityTests;

[TestFixture]
public class EmailEntityTests
{
    [Test]
    public void Create_EmailEntityWithValidParameters_ReturnsEmailEntityWithPropertiesSet()
    {
        // Arrange
        var recipients = new List<EmailAddress>
        {
            EmailAddress.Create("recipient1@example.com"),
            EmailAddress.Create("recipient2@example.com")
        };

        var subject = EmailSubjectLine.Create("Test Subject");
        var body = EmailBodyText.Create("This is the email body.");

        // Act
        var emailEntity = EmailEntity.Create(recipients, subject, body);

        // Assert
        Assert.IsNotNull(emailEntity);
        Assert.That(emailEntity.Type, Is.EqualTo(EmailType.Email));
        Assert.That(emailEntity.EmailStatus, Is.EqualTo(EmailStatus.UnRead));
        CollectionAssert.AreEqual(recipients, emailEntity.Recipients);
        Assert.That(emailEntity.Subject, Is.EqualTo(subject));
        Assert.That(emailEntity.Body, Is.EqualTo(body));
    }
}
