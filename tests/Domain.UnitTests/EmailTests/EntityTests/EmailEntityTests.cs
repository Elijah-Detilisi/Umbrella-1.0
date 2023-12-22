namespace Domain.UnitTests.EmailTests.EntityTests;

[TestFixture]
public class EmailEntityTests
{
    [Test]
    public void Create_EmailEntity_WithValidParameters_Returns_EmailEntity_WithPropertiesSet()
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
        Assert.Multiple(() =>
        {
            Assert.That(emailEntity, Is.Not.Null);
            Assert.That(emailEntity.Id, Is.EqualTo(0));
            Assert.That(emailEntity.Body, Is.EqualTo(body));
            Assert.That(emailEntity.Subject, Is.EqualTo(subject));
            Assert.That(emailEntity.Type, Is.EqualTo(EmailType.Email));
            Assert.That(emailEntity.EmailStatus, Is.EqualTo(EmailStatus.UnRead));
        });

        CollectionAssert.AreEqual(recipients, emailEntity.Recipients);
       
    }
}
