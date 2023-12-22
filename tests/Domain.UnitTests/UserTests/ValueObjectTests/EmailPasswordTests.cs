using Domain.User.Exceptions;

namespace Domain.UnitTests.UserTests.ValueObjectTests;

[TestFixture]
public class EmailPasswordTests
{
    [Test]
    public void Create_ValidPassword_ShouldSucceed()
    {
        // Arrange
        string validPassword = "ValidPassword123";

        // Act
        var emailPassword = EmailPassword.Create(validPassword);

        // Assert
        Assert.That(emailPassword.ToString(), Is.EqualTo(validPassword));
    }

    [TestCase("123")]
    [TestCase("short")]
    public void Create_ShortPassword_ShouldThrow_PasswordTooShortException(string invalidPassword)
    {
        // Act & Assert
        Assert.Throws<PasswordTooShortException>(() => EmailPassword.Create(invalidPassword));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Create_EmptyPassword_ShouldThrowEmptyValueException(string invalidPassword)
    {
        // Act & Assert
        Assert.Throws<EmptyValueException>(() => EmailPassword.Create(invalidPassword));
    }

    [Test]
    public void Equals_TwoEqual_EmailPasswords_ShouldReturnTrue()
    {
        // Arrange
        var emailPassword1 = EmailPassword.Create("ValidPassword123");
        var emailPassword2 = EmailPassword.Create("ValidPassword123");

        // Act & Assert
        Assert.That(emailPassword2, Is.EqualTo(emailPassword1));
    }

    [Test]
    public void Equals_TwoDifferent_EmailPasswords_ShouldReturnFalse()
    {
        // Arrange
        var emailPassword1 = EmailPassword.Create("ValidPassword123");
        var emailPassword2 = EmailPassword.Create("AnotherPassword456");

        // Act & Assert
        Assert.That(emailPassword2, Is.Not.EqualTo(emailPassword1));
    }
}