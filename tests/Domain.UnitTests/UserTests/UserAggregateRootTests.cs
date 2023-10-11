namespace Domain.UnitTests.UserTests;

[TestFixture]
public class UserAggregateRootTests
{
    [Test]
    public void Create_UserWithValidEmailAddressAndPassword_ShouldSucceed()
    {
        // Arrange
        var username = "newUsername";
        var emailAddress = EmailAddress.Create("user@example.com");
        var emailPassword = EmailPassword.Create("Password123");

        // Act
        var user = UserAggregateRoot.Create(emailAddress, emailPassword, username);

        // Assert
        Assert.That(user, Is.Not.Null);
        Assert.That(user.EmailAddress, Is.EqualTo(emailAddress));
        Assert.That(user.EmailPassword, Is.EqualTo(emailPassword));
        Assert.That(user.UserName, Is.Empty);
    }
}
