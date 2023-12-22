namespace Domain.UnitTests.UserTests.EntityTests;

[TestFixture]
public class UserEntityTests
{
    [Test]
    public void Create_User_WithValidParameters_Returns_UserObject_WithPropertiesSet()
    {
        // Arrange
        string username = "testuser";
        var emailAddress = EmailAddress.Create("user@example.com");
        var emailPassword = EmailPassword.Create("Password123");

        // Act
        var user = UserEntity.Create(emailAddress, emailPassword, username);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Id, Is.EqualTo(0));
            Assert.That(user.UserName, Is.EqualTo(username));
            Assert.That(user.EmailAddress, Is.EqualTo(emailAddress));
            Assert.That(user.EmailPassword, Is.EqualTo(emailPassword));
        });
    }
}
