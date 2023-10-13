namespace Domain.UnitTests.EmailTests.ValueObjectTests;

[TestFixture]
public class EmailAddressTests
{
    [Test]
    public void Create_ValidEmailAddress_ShouldSucceed()
    {
        // Arrange
        string validEmail = "test@example.com";

        // Act
        var emailAddress = EmailAddress.Create(validEmail);

        // Assert
        Assert.That(emailAddress.ToString(), Is.EqualTo(validEmail));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("invalid-email")]
    [TestCase("missing@dotcom")]
    public void Create_InvalidEmailAddress_ShouldThrowArgumentException(string invalidEmail)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailAddress.Create(invalidEmail));
    }

    [Test]
    public void Equals_TwoEqualEmailAddresses_ShouldReturnTrue()
    {
        // Arrange
        var emailAddress1 = EmailAddress.Create("test@example.com");
        var emailAddress2 = EmailAddress.Create("test@example.com");

        // Act & Assert
        Assert.That(emailAddress2, Is.EqualTo(emailAddress1));
    }

    [Test]
    public void Equals_TwoDifferentEmailAddresses_ShouldReturnFalse()
    {
        // Arrange
        var emailAddress1 = EmailAddress.Create("test1@example.com");
        var emailAddress2 = EmailAddress.Create("test2@example.com");

        // Act & Assert
        Assert.That(emailAddress2, Is.Not.EqualTo(emailAddress1));
    }
}
