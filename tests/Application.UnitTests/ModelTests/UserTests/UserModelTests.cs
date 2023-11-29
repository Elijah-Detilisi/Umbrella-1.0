using Application.User.Models;
using Domain.User.ValueObjects;

namespace Application.UnitTests.ModelTests.UserTests;

[TestFixture]
public class UserModelTests
{
    [Test]
    public void SetProperties_UserModelWithValidValues_PropertiesSetCorrectly()
    {
        // Arrange
        var emailAddress = EmailAddress.Create("user@example.com");
        var emailPassword = EmailPassword.Create("securepassword");
        var userName = "TestUser";

        // Act
        var userModel = new UserModel
        {
            EmailAddress = emailAddress,
            EmailPassword = emailPassword,
            UserName = userName
        };

        // Assert
        Assert.That(userModel, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(userModel.EmailAddress, Is.EqualTo(emailAddress));
            Assert.That(userModel.EmailPassword, Is.EqualTo(emailPassword));
            Assert.That(userModel.UserName, Is.EqualTo(userName));
        });
    }
}
