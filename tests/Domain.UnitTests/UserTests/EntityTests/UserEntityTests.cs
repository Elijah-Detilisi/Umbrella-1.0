namespace Domain.UnitTests.UserTests.EntityTests;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void Create_UserWithValidParameters_ReturnsUserObjectWithPropertiesSet()
    {
        // Arrange
        string username = "testuser";
        var emailAddress = EmailAddress.Create("user@example.com");
        var emailPassword = EmailPassword.Create("Password123");

        // Act
        var user = UserEntity.Create(emailAddress, emailPassword, username);

        // Assert
        Assert.That(user, Is.Not.Null);
        Assert.That(user.UserName, Is.EqualTo(username));
        Assert.That(user.EmailAddress, Is.EqualTo(emailAddress));
        Assert.That(user.EmailPassword, Is.EqualTo(emailPassword));
    }
}
