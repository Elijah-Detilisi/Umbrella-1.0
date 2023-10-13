using Domain.Common.Exceptions;

namespace Domain.UnitTests.CommonTests.ValueObjectTests;

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
    public void Create_EmptyEmailAddress_ShouldThrowEmptyValueException(string invalidEmail)
    {
        // Act & Assert
        Assert.Throws<EmptyValueException>(() => EmailAddress.Create(invalidEmail));
    }


    [TestCase("invalid-email")]
    [TestCase("missing@dotcom")]
    public void Create_InvalidEmailAddress_ShouldThrowInvalidEmailAddressException(string invalidEmail)
    {
        // Act & Assert
        Assert.Throws<InvalidEmailAddressException>(() => EmailAddress.Create(invalidEmail));
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
