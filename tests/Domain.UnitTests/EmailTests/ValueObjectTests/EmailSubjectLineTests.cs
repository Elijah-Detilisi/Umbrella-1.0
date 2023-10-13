namespace Domain.UnitTests.EmailTests.ValueObjectTests;

[TestFixture]
public class EmailSubjectLineTests
{
    [Test]
    public void Create_ValidSubjectLine_ShouldSucceed()
    {
        // Arrange
        string validSubject = "Important Update";

        // Act
        var subjectLine = EmailSubjectLine.Create(validSubject);

        // Assert
        Assert.That(subjectLine, Is.Not.Null);
        Assert.That(subjectLine.ToString(), Is.EqualTo(validSubject));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Create_EmptyOrNullOrWhiteSpaceSubjectLine_ShouldThrowArgumentException(string invalidSubject)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailSubjectLine.Create(invalidSubject));
    }

    [Test]
    public void Create_SubjectLineExceedsMaxLength_ShouldThrowArgumentException()
    {
        // Arrange
        var longSubject = new string('X', 101);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailSubjectLine.Create(longSubject));
    }

    [Test]
    public void Create_SubjectLineWithNewlinesOrCarriageReturns_ShouldThrowArgumentException()
    {
        // Arrange
        string subjectWithNewlines = "Subject with\nnewline";
        string subjectWithCarriageReturn = "Subject with\rreturn";
        string subjectWithBoth = "Subject with\nnewline\rreturn";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => EmailSubjectLine.Create(subjectWithNewlines));
        Assert.Throws<ArgumentException>(() => EmailSubjectLine.Create(subjectWithCarriageReturn));
        Assert.Throws<ArgumentException>(() => EmailSubjectLine.Create(subjectWithBoth));
    }
}