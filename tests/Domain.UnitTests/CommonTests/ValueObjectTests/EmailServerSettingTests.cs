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
        Assert.IsNotNull(emailServerSetting);
        Assert.AreEqual(portNumber, emailServerSetting.Value.Item1);
        Assert.AreEqual(serverName, emailServerSetting.Value.Item2);
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
        Assert.IsTrue(emailServerSetting1.Equals(emailServerSetting2));
        //Assert.IsTrue(emailServerSetting1 == emailServerSetting2);
        //Assert.IsFalse(emailServerSetting1 != emailServerSetting2);
    }

    [Test]
    public void Equals_TwoDifferentEmailServerSettings_ReturnsFalse()
    {
        // Arrange
        var emailServerSetting1 = EmailServerSetting.Create(587, "smtp.example.com");
        var emailServerSetting2 = EmailServerSetting.Create(465, "smtp.example.com");

        // Act & Assert
        Assert.IsFalse(emailServerSetting1.Equals(emailServerSetting2));
        Assert.IsFalse(emailServerSetting1 == emailServerSetting2);
        Assert.IsTrue(emailServerSetting1 != emailServerSetting2);
    }
}
