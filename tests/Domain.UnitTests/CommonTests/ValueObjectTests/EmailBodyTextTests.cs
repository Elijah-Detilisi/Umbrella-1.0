namespace Domain.UnitTests.CommonTests.ValueObjectTests;

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
    public void Create_EmptyOrNullOrWhiteSpaceEmailBody_ShouldThrowArgumentException(string invalidBody)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailBodyText.Create(invalidBody));
    }

    [Test]
    public void Create_EmailBodyExceedsMaxLength_ShouldThrowArgumentException()
    {
        // Arrange
        string longBody = new string('X', 1001);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailBodyText.Create(longBody));
    }
}