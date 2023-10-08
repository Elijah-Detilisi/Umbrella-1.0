namespace Domain.UnitTests.User.ValueObjectTests;

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

    [TestCase(null)]
    [TestCase("")]
    [TestCase("short")]
    public void Create_InvalidPassword_ShouldThrowArgumentException(string invalidPassword)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailPassword.Create(invalidPassword));
    }

    [Test]
    public void Equals_TwoEqualEmailPasswords_ShouldReturnTrue()
    {
        // Arrange
        var emailPassword1 = EmailPassword.Create("ValidPassword123");
        var emailPassword2 = EmailPassword.Create("ValidPassword123");

        // Act & Assert
        Assert.That(emailPassword2, Is.EqualTo(emailPassword1));
    }

    [Test]
    public void Equals_TwoDifferentEmailPasswords_ShouldReturnFalse()
    {
        // Arrange
        var emailPassword1 = EmailPassword.Create("ValidPassword123");
        var emailPassword2 = EmailPassword.Create("AnotherPassword456");

        // Act & Assert
        Assert.That(emailPassword2, Is.Not.EqualTo(emailPassword1));
    }
}