namespace Domain.UnitTests.EmailTests.ValueObjectTests;

[TestFixture]
public class EmailBodyTextTests
{
    [Test]
    public void Create_Valid_EmailBody_ShouldSucceed()
    {
        // Arrange
        string validBody = "This is the email body.";

        // Act
        var emailBody = EmailBodyText.Create(validBody);

        // Assert
        Assert.That(emailBody, Is.Not.Null);
        Assert.That(emailBody.ToString(), Is.EqualTo(validBody));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Create_Empty_Or_Null_Or_WhiteSpace_EmailBody_ShouldThrow_EmptyValueException(string invalidBody)
    {
        // Act & Assert
        Assert.Throws<EmptyValueException>(() => EmailBodyText.Create(invalidBody));
    }

    [Test]
    public void Create_EmailBody_Exceeds_MaxLength_ShouldThrow_EmailBodyTooLongException()
    {
        // Arrange
        string longBody = new('X', 1001);

        // Act & Assert
        Assert.Throws<EmailBodyTooLongException>(() => EmailBodyText.Create(longBody));
    }
}