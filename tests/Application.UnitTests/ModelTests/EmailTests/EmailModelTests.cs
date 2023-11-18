namespace Application.UnitTests.ModelTests.EmailTests;

[TestFixture]
public class EmailModelTests
{
    [Test]
    public void SetProperties_EmailModelWithValidValues_PropertiesSetCorrectly()
    {
        // Arrange
        var sender = EmailAddress.Create("sender@example.com");
        var recipients = new List<EmailAddress>
        {
            EmailAddress.Create("recipient1@example.com"),
            EmailAddress.Create("recipient2@example.com")
        };

        var subject = EmailSubjectLine.Create("Test Subject");
        var body = EmailBodyText.Create("This is the email body.");

        // Act
        var emailModel = new EmailModel
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Body = body,
            Subject = subject,
            Sender = sender,
            Recipients = recipients
        };

        // Assert
        Assert.That(emailModel, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(emailModel.Type, Is.EqualTo(EmailType.Email));
            Assert.That(emailModel.EmailStatus, Is.EqualTo(EmailStatus.UnRead));
            Assert.That(emailModel.Body, Is.EqualTo(body));
            Assert.That(emailModel.Subject, Is.EqualTo(subject));
            Assert.That(emailModel.Sender, Is.EqualTo(sender));
        });
        CollectionAssert.AreEqual(recipients, emailModel.Recipients);
    }
}
