using Domain.Common.Exceptions;

namespace Domain.UnitTests.CommonTests.ValueObjectTests;

[TestFixture]
public class EmailServerSettingTests
{
    [Test]
    public void Create_EmailServerSettingWithValidParameters_ReturnsEmailServerSettingWithPropertiesSet()
    {
        // Arrange
        int portNumber = 587;
        string serverName = "smtp.example.com";

        // Act
        var emailServerSetting = EmailServerSetting.Create(portNumber, serverName);

        // Assert
        Assert.That(emailServerSetting, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(emailServerSetting.Value.Item1, Is.EqualTo(portNumber));
            Assert.That(emailServerSetting.Value.Item2, Is.EqualTo(serverName));
        });
    }

    [Test]
    public void Create_EmailServerSettingWithEmptyServerName_ThrowsEmptyValueException()
    {
        // Arrange
        int portNumber = 587;
        string serverName = string.Empty;

        // Act & Assert
        Assert.Throws<EmptyValueException>(() => EmailServerSetting.Create(portNumber, serverName));
    }

    [Test]
    public void Create_EmailServerSettingWithZeroPortNumber_ThrowsArgumentException()
    {
        // Arrange
        int portNumber = 0;
        string serverName = "smtp.example.com";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailServerSetting.Create(portNumber, serverName));
    }

    [Test]
    public void Equals_TwoEqualEmailServerSettings_ReturnsTrue()
    {
        // Arrange
        var emailServerSetting1 = EmailServerSetting.Create(587, "smtp.example.com");
        var emailServerSetting2 = EmailServerSetting.Create(587, "smtp.example.com");

        // Act & Assert
        Assert.That(emailServerSetting1, Is.EqualTo(emailServerSetting2));
    }

    [Test]
    public void Equals_TwoDifferentEmailServerSettings_ReturnsFalse()
    {
        // Arrange
        var emailServerSetting1 = EmailServerSetting.Create(587, "smtp.example.com");
        var emailServerSetting2 = EmailServerSetting.Create(465, "smtp.example.com");

        // Act & Assert
        Assert.That(emailServerSetting1, Is.Not.EqualTo(emailServerSetting2));
        Assert.That(emailServerSetting1, Is.Not.EqualTo(emailServerSetting2));
        Assert.That(emailServerSetting1, Is.Not.EqualTo(emailServerSetting2));
    }
}
