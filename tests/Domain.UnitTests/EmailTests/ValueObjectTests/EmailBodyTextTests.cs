using Domain.Common.Exceptions;
using Domain.Email.Exceptions;

namespace Domain.UnitTests.EmailTests.ValueObjectTests;

[TestFixture]
public class EmailBodyTextTests
{
    [Test]
    public void Create_ValidEmailBody_ShouldSucceed()
    {
        // Arrange
        string validBody = "This is the email body.";

        // Act
        var emailBody = EmailBodyText.Create(validBody);

        // Assert
        Assert.NotNull(emailBody);
        Assert.That(emailBody.ToString(), Is.EqualTo(validBody));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Create_EmptyOrNullOrWhiteSpaceEmailBody_ShouldThrowEmptyValueException(string invalidBody)
    {
        // Act & Assert
        Assert.Throws<EmptyValueException>(() => EmailBodyText.Create(invalidBody));
    }

    [Test]
    public void Create_EmailBodyExceedsMaxLength_ShouldThrowEmailBodyTooLongException()
    {
        // Arrange
        string longBody = new string('X', 1001);

        // Act & Assert
        Assert.Throws<EmailBodyTooLongException>(() => EmailBodyText.Create(longBody));
    }
}