namespace Domain.UnitTests.User;

[TestFixture]
public class UserAggregateRootTests
{
    [Test]
    public void Create_UserWithValidEmailAddressAndPassword_ShouldSucceed()
    {
        // Arrange
        var emailAddress = EmailAddress.Create("user@example.com");
        var emailPassword = EmailPassword.Create("Password123");

        // Act
        var user = UserAggregateRoot.Create(emailAddress, emailPassword);

        // Assert
        Assert.That(user, Is.Not.Null);
        Assert.That(user.EmailAddress, Is.EqualTo(emailAddress));
        Assert.That(user.EmailPassword, Is.EqualTo(emailPassword));
        Assert.That(user.UserName, Is.Empty);
    }

    [Test]
    public void ChangeUserName_NewUsername_ShouldChangeUserName()
    {
        // Arrange
        var emailAddress = EmailAddress.Create("user@example.com");
        var emailPassword = EmailPassword.Create("Password123");
        var user = UserAggregateRoot.Create(emailAddress, emailPassword);

        // Act
        user.ChangeUserName("newUsername");

        // Assert
        Assert.That(user.UserName, Is.EqualTo("newUsername"));
    }
}
